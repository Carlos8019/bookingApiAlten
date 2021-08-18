/*
    Cette fonction permet faire un mapping des donnees au format JSON pour l'envoyer 
    dans le demande au service web
 */
function ClienteDto(props) {
    let userName=props.email;
    let password=props.password;
    const client={userName,password};
    //utiliser JSON.stringify pour transformer au formar JSON
    return JSON.stringify(client);
}
export default ClienteDto;