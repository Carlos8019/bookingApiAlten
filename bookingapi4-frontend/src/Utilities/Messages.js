/*cette fichier permet la centralisation des messages aux utilisateurs
avec le code vous accédez au message souhaité.
 */
export const COMPTE_ERROR_CREATION="Erreur de création de compte, veuillez réessayer.";
export const COMPTE_ERROR_LOGIN="Courriel ou mot de passe incorrect, veuillez réessayer.";
export const COMPTE_ERROR_LOGIN_COMMUNICATION="Erreur de connexion, veuillez réessayer."
export const RESERVATION_ERROR="Erreur de création de reservation, veuillez réessayer.";
export const RESERVATION_SUCCESS="Réservation créée avec succès";
export const RESERVATION_MAX_DAYS="Les dates saisies doivent être comprises dans un délai de 3 jours maximum.";
export const RESERVATION_START_DATE_ERROR="La date initialle fait partie d'autre reservation.";
export const RESERVATION_END_DATE_ERROR="La date de fin fait partie d'autre reservation.";
export const RESERVATION_ROOM_ERROR="La chambre d'hôtel sélectionnée n'existe pas.";
export const RESERVATION_NUMBER_ERROR="La réservation sélectionnée n'existe pas.";
export const RESERVATION_DELETE_ERROR="Impossible de supprimer la réservation, essayez à nouveau.";
export const RESERVATION_DELETE_SUCCESS="La réservation a été supprimée avec succès.";
export const RESERVATION_EDIT_ERROR="Erreur de modification de compte, veuillez réessayer.";
export const RESERVATION_EDIT_SUCCESS="Réservation modifié avec succès";
export const RESERVATION_ERROR_DAYS_ADVANCE="Les réservations doivent être effectuées moins de 30 jours à l'avance.";
/*
export default function Messages(code) {
    const jsonMessages={
        RESERVATION_ERROR:"Les dates saisies doivent être comprises dans un délai de 3 jours maximum.",
        RESERVATION_SUCCESS:"réservation créée avec succès"
    }
    
        return jsonMessages.code;
}*/
