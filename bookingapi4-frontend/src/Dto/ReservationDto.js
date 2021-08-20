/*
    Cette fonction permet faire un mapping des donnees au format JSON pour l'envoyer 
    dans le demande au service web de reservation
 */
    function ReservationDto(props) {
        let idReservation=props.idReservation;
        let userName=props.userName;
        let startDate=props.startDate;
        let endDate=props.endDate;
        let statusReservation=props.statusReservation;
        let room=props.room;
        const reservation={idReservation,userName,startDate,endDate,statusReservation,room};
        //utiliser JSON.stringify pour transformer au formar JSON
        return JSON.stringify(reservation);
    }
    export default ReservationDto;