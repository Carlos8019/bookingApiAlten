import  React, { useState, useEffect, useContext } from 'react'
import ReservationContext from '../Contexts/ReservationContext';
//library de gestion d'invocation des web api GET
import GetData from '../Utilities/ApiServiceGet';
//path de chaque petition
import * as constants from '../Utilities/Constants';
//icons library
import { EmojiLaughing, EmojiFrown } from 'react-bootstrap-icons';
/*
cette fonctione gestione la disponibilite des chambres des prochines max jours qui sont parametres du web api
*/
export default function CheckRooms() {
    //utiliser le context por configurer les dates
    const { formatDate } = useContext(ReservationContext);
    //array avec les dates et la disponibilite
    const [tableAvailability,setTableAvailability]=useState([])
    //invoquer au web api 
    const GetAvailability=async()=>{
        //faire la petition et gestioner la promise
        await GetData(constants.API_GET_AVAILABILITY)
        .then((response)=>{
            //ajouter les donnees au array
            setTableAvailability(response.data);
        })
        .catch(error=>{});
    }
    useEffect(() => {
        //charger a la premiere fois
        GetAvailability();
    }, [])
    return (
        <div className="container">
        <table className="table table-striped">
        <thead>
            <tr>
                <th>Jour</th>
                <th>Disponibilité</th>
                <th>Disponibilité</th>
            </tr>
        </thead>
        <tbody>
        {tableAvailability.map(dates => (
                <tr key={dates.idCalendar}>
                    <td>{formatDate(dates.date,1)}</td>
                    <td>{dates.status===1?<EmojiLaughing color="green" size={16}/>:<EmojiFrown color="red" size={16}/>}</td>
                    <td>{dates.status===1?<h6>Chambre disponible</h6>:<h6>Chambre non disponible</h6>}</td>
                </tr>
            ))}
        </tbody>
    </table>
    </div>
    )
}