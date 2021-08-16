//utiliser axios pour faire des demandes au web service
import axios from 'axios';
//Constant, Il a le link base principal du web service
import { baseURL } from './Constants';
//cette fonction permet demander au web service par le biais d'une pétition post qui envoye un JSON
//Elle retourne un promise pour etre gestione dans le context
export default async function postData(path,json){
     return await axios.post(baseURL+path,json,{headers: {
        'Content-Type': 'application/json'
      }});
}