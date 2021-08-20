import { createContext, useContext, useState } from "react"
//gestion du json
import ReservationDto from "../Dto/ReservationDto";
//Context d'ecran modal
import ModalContext from "./ModalContext";
//gestion de format aux dates
import Moment from 'moment';
//gestion d'invocation au web api
import postData from "../Utilities/ApiServicePost";
import GetData from '../Utilities/ApiServiceGet';
//messages 
import * as messages from '../Utilities/Messages';
import * as constant from '../Utilities/Constants';
//creation du Context
const ReservationContext = createContext();
//creation de TAG et propaguer aux les children components
const ReservationProvider = ({ children }) => {
    //obtenir les dates de la reservation du component
    const [valueDates, setValueDates] = useState([null, null]);
    //gestioner le button du modal avec le contextModal, 
    const { flagEdit, setEnableButton, setMessageModal, toggleModal, userName } = useContext(ModalContext);
    //obtenir le userName pour enregistrer la reservation
    //const {userName}=useContext(UserContext);

    //array pour charger les reservations du client
    const [tableReservation, setTableReservation] = useState([]);
    //code du chambre
    const [room, setRoom] = useState("001");
    //gestioner le changements des dates
    const handleChangeDates = (newValue) => {
        setValueDates(newValue, ValidateDates(newValue));
    }
    //valider les inputs des dates
    const ValidateDates = (datesArray) => {
        if (datesArray[0] !== null && datesArray[1] !== null)
            setEnableButton(false);
        else
            setEnableButton(true);
    }
    //fonction pour charguer des le webapi
    const getDataReservation = async () => {
        await GetData(constant.API_GET_AVAILABLE_BY_USER + userName)
            .then((response) => {
                setTableReservation(response.data);
            })
            .catch((error) => {
            });
    }
    //creation de une reservation
    const CreateReservation = () => {
        //gestione le constant du path du web api de creation ou modification
        var constantWebAPI = (flagEdit === "0") ? constant.API_NEW_RESERVATION : constant.API_EDIT_RESERVATION;
        //transformer des dates au format yyyymmdd qui est utilise dans le web api
        let startDate = Moment(valueDates[0]).format(constant.DATES_WEBAPI_FORMAT);
        let endDate = Moment(valueDates[1]).format(constant.DATES_WEBAPI_FORMAT);
        let statusReservation = "1";
        //modifier l'index de reservation d'accord a une modification ou une creation
        let idReservation = (flagEdit === "0") ? constant.idReservation : flagEdit;
        //creation du DTO pour envoyer au web api en JSON
        const reservation = ReservationDto({ idReservation, userName, startDate, endDate, statusReservation, room });
        console.log(reservation);
        console.log(flagEdit);
        //invoque au web service
        postData(constantWebAPI, reservation)
            .then((response) => {
                //gestioner la response avec les messages
                if (response.data === 3)
                    setMessageModal(messages.RESERVATION_ROOM_ERROR);
                if (response.data === 2)
                    setMessageModal((flagEdit === "0") ? messages.RESERVATION_ERROR : messages.RESERVATION_EDIT_ERROR);
                if (response.data === 1)
                    setMessageModal((flagEdit === "0") ? messages.RESERVATION_SUCCESS : messages.RESERVATION_EDIT_SUCCESS);
                if (response.data === 0)
                    setMessageModal(messages.RESERVATION_MAX_DAYS);
                if (response.data === -1)
                    setMessageModal(messages.RESERVATION_START_DATE_ERROR);
                if (response.data === -2)
                    setMessageModal(messages.RESERVATION_END_DATE_ERROR);
                if (response.data === -3)
                    setMessageModal(messages.RESERVATION_ERROR_DAYS_ADVANCE);

                //actualisation des donnees
                getDataReservation()
                toggleModal(3);
            })
            .catch(error => {
                toggleModal(3);
                setMessageModal(messages.RESERVATION_ERROR);
            });
    }
    //gestion des formats des dates
    const formatDate = (date, option) => {
        // envoyer le format qui corresponde d'accord a l'option
        //option=1 pour afficher sur l'écran
        //option=2 pour envoyer au web api
        if (option === 1) {
            //var formatShow = 
            return Moment(date, constant.DATES_WEBAPI_FORMAT).format(constant.DATES_SHOW);
            //return formatShow.format(constant.DATES_SHOW);
        }
        else {
            //var formatShow = 
            return Moment(date, constant.DATES_WEBAPI_FORMAT).format(constant.DATES_WEBAPI_FORMAT);
            //return formatShow.format(constant.DATES_WEBAPI_FORMAT);
        }

    }
    //Gestioner les buttons edit et delete
    //option 2=supprimer
    //option 1=edit
    const handleClick = (e, option, id) => {
        e.nativeEvent.stopImmediatePropagation();
        //obtenir les donnes pour modification 
        var items = tableReservation.filter(index => index.idReservacion === id);
        console.log("items", items);
        //transformer des dates au format yyyymmdd qui est utilise dans le web api
        let startDate = formatDate(items[0].startDate, 2);
        let endDate = formatDate(items[0].endDate, 2);
        let statusReservation = "1";
        let idReservation = items[0].idReservacion + "";
        //creation du DTO pour envoyer au web api en JSON
        const reservation = ReservationDto({ idReservation, userName, startDate, endDate, statusReservation, room });
        //console.log("click", reservation);
        if (option === 1)//edition
        {
            //transformer au formate du calendar
            let startDateCalendar = formatDate(items[0].startDate, 1);
            let endDateCalendar = formatDate(items[0].endDate, 1);
            //actualisation d'etat 
            setValueDates([startDateCalendar, endDateCalendar]);
            handleChangeDates([startDateCalendar, endDateCalendar]);
            //invoque la fenêtre modale en spécifiant le paramètre de modification facultatif
            toggleModal(3, 1, idReservation);
        }
        if (option === 2)//supprimer
        {
            postData(constant.API_DELETE_RESERVATION, reservation)
                .then((response) => {
                    //gestioner la reponse selon le code du web api
                    if (response.data === 3)
                        setMessageModal(messages.RESERVATION_NUMBER_ERROR);
                    if (response.data === 2)
                        setMessageModal(messages.RESERVATION_DELETE_ERROR);
                    if (response.data === 1)
                        setMessageModal(messages.RESERVATION_DELETE_SUCCESS);
                    //actualisation des donnees
                    getDataReservation();
                })
                .catch(error => {
                    setMessageModal(messages.RESERVATION_DELETE_ERROR);
                });
        }
    }
    //variables et fonctions d'exportation
    const data = { handleClick, room, formatDate, valueDates, CreateReservation, setValueDates, handleChangeDates, getDataReservation, tableReservation }
    return (
        <ReservationContext.Provider value={data}>
            {children}
        </ReservationContext.Provider>
    )
}
export { ReservationProvider }
export default ReservationContext;