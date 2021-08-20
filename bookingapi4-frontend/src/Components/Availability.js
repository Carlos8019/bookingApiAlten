import React, { useContext, useEffect } from 'react'
import { Button } from 'reactstrap'
//utiliser le context pour les ecrans modals
import ModalContext from '../Contexts/ModalContext'
//utiliser le context pour gestioner les reservations
import ReservationContext from '../Contexts/ReservationContext';
//Component de creation de reservation modal
import Reservation from './Reservation';


function Availability() {
    //invoquer les fonctions et les variables du context
    const { getDataReservation, tableReservation,handleClick, formatDate, room } = useContext(ReservationContext);
    const { toggleModal, messageModal, setMessageModal } = useContext(ModalContext);
    //chargement des reservations
    useEffect(() => {
        getDataReservation();
        setMessageModal("");
    }, []);
    return (
        <div className="container">
            <h4>Reservations</h4>
            <div className="col-md-2">
                <label>Chambres disponibles</label>
                <select className="form-select" aria-label="Sélectionnez une chambre">
                    <option selected value={room}>001</option>
                </select>
                <br />
                <Button onClick={() => toggleModal(3)}>Réserver une date</Button>
            </div>
            {messageModal ? <p className="alert alert-info">{messageModal}</p> : <p></p>}
            <Reservation />
            <table className="table table-striped">
                <thead>
                    <tr>
                        <th>Courriel</th>
                        <th>Date de début</th>
                        <th>Date de fin</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {tableReservation.map(client => (
                        <tr key={client.idReservacion}>
                            <td>{client.clients.userName}</td>
                            <td>{formatDate(client.startDate,1)}</td>
                            <td>{formatDate(client.endDate,1)}</td>
                            <td>
                                <button className="btn btn-primary" onClick={(e)=>handleClick(e,1,client.idReservacion)} >Edit</button>{"  "}
                                <button className="btn btn-danger" onClick={(e)=>handleClick(e,2,client.idReservacion)}>Delete</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
            <Reservation />
        </div>
    )
}
export default Availability