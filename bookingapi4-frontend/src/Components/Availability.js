import React, { useContext, useEffect } from 'react'
import { Button } from 'reactstrap'
import ModalContext from '../Contexts/ModalContext'
import { UserContext } from '../Contexts/UserContext';
import Reservation from './Reservation';

function Availability() {
    const {userName,setUserName}=useContext(UserContext);
    useEffect(() => {
        setUserName("carlosyanez2009@gmail.com");
    })
    const {toggleModal}= useContext(ModalContext);
    return (
        <div>
            <Button onClick={() => toggleModal(3)}>Réserver une date</Button>
            <Reservation />
        </div>
    )
}

export default Availability