import { createContext, useState } from "react";
import ClienteDto from "../Dto/ClientDto";
//pour crypter le mot de passe
import sha256 from 'js-sha256';
import postData from "../Utilities/ApiServicePost";
//Le Context permet reutiliser la fonctionalite de l'ecran modal avec tous les components
//d'accord a le choix se enregistre une compte ou l'acces est valide
const ModalContext=createContext();
//Le modalProvider s'utilise pour mettre en relation le context avec chaque component 
//En ce cas on va l'assigner dan le fichier Index.js
const ModalProvider=({children})=>{
    //gestioner l'ecran modal
    const [modal, setModal] = useState(false);
    const toggleModal = () => setModal(!modal);
    //gestioner les message de validation
    const [messageModal,setMessageModal]=useState("");
    //gestioner le button de creation 
    const[enableButton,setEnableButton]=useState(true);
    //courriel et mot de passe pour s'inscrire 
    const [form,setForm]=useState({
        email:'',
        password:''
    });
    //faire une demande au web service et valider l'acces
    const ValiderLogin=(props, e)=>{
        e.nativeEvent.stopImmediatePropagation();
        const email=form.email;
        //crypter le mot de passe avec sha256
        const password=sha256(form.password);
        const client = ClienteDto({ email,password });
        console.log("form",form," client",client);
        //faire le demande au web service et gestioner le promise comme un resultat
        postData("createUser",client)
        .then((response)=> {
              console.log(response);
        })
        .catch((error)=>{
              console.log("error",error)
        });
        
    }
    //exporter les etast et les fonctions avec les autres components
    const data={modal,form,messageModal,enableButton,setEnableButton,setMessageModal,toggleModal,ValiderLogin,setForm}
    return(
        <ModalContext.Provider value={data}>
            {children}
        </ModalContext.Provider>
    )
}
export default ModalContext;
export {ModalProvider}