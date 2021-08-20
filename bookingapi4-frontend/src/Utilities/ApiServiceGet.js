import axios from "axios";
import { baseURL } from "./Constants";
export default async function GetData(path)
{
    return await axios.get(baseURL+path)
}