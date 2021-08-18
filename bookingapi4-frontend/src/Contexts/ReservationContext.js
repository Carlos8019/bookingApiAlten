import { createContext, useContext,useState } from "react"
import ReservationDto from "../Dto/ReservationDto";
import ModalContext from "./ModalContext";
import { UserContext } from "./UserContext";

const ReservationContext=createContext();
const ReservationProvider=({children})=>{
    //obtenir les dates de la reservation du component
    const [valueDates, setValueDates] = useState([null, null]);
    //gestioner le button du modal avec le contextModal, 
    const {setEnableButton}=useContext(ModalContext);    
    //obtenir le userName pour enregistrer la reservation
    const {userName}=useContext(UserContext);
    //gestioner le changements des dates
    const handleChangeDates=(newValue)=>{
        setValueDates(newValue,ValidateDates(newValue));
    }
    const ValidateDates=(datesArray)=>{
        if(datesArray[0]!==null && datesArray[1]!==null)
            setEnableButton(false);
        else
            setEnableButton(true);
    }
    const CreateReservation=()=>{
        let startDate=valueDates[0];
        let endDate=valueDates[1];
        let status="1";
        let room="1";
        const reservation=ReservationDto({userName,startDate,endDate,status,room});
        console.log(reservation);
    }
    const data={CreateReservation,valueDates,setValueDates,handleChangeDates}
    return(
        <ReservationContext.Provider value={data}>
            {children}
        </ReservationContext.Provider>
    )
}
export {ReservationProvider}
export default ReservationContext;