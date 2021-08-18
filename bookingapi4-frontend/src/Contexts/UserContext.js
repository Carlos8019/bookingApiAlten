import { createContext, useState } from "react";

const UserContext=createContext();
const UserProvider=({children})=>{
    const [userName,setUserName]=useState("");
    const data={userName,setUserName}
    return (
        <UserProvider value={data}>
            {children}
        </UserProvider>
    )
}
export default UserProvider;
export {UserContext}