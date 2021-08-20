import axios from "axios";
//parametres 
import { baseURL } from "./Constants";
//gestion d'invocation au web api avec GET async avec axios, retourner une promise
export default async function GetData(path)
{
    return await axios.get(baseURL+path)
}