import { createContext, useState } from "react";
//pour la validation du format du courriel
import validator from 'validator';
import ClienteDto from "../Dto/ClientDto";
//pour crypter le mot de passe
import sha256 from 'js-sha256';
//library avec la funcionalite Post
import postData from "../Utilities/ApiServicePost";
//constants pour montrer des messagess
import * as constant from '../Utilities/Constants';
import * as messages from '../Utilities/Messages';
//Le Context permet reutiliser la fonctionalite de l'ecran modal avec tous les components
//d'accord a le choix se enregistre une compte ou l'acces est valide
const ModalContext=createContext();
//Le modalProvider s'utilise pour mettre en relation le context avec chaque component 
//En ce cas on va l'assigner dans le fichier Index.js
const ModalProvider=({children})=>{

    //gestioner l'ecran modal
    const [modal, setModal] = useState(false);
    //gestioner l'ecran modal du login
    const[modalLogin,setModalLogin]=useState(false);
    const toogleModalLogin=()=>setModalLogin(!modal);
    //gesioner l'ecran modal de la reservation
    const [modalReservation,setModalReservation]=useState(false);
    //gestioner l'utilisateur 
    const [userName,setUserName]=useState("");
    //flagEdit est utilise pour reutiliser l'ecran modal et pour savoir s'il est une modification
    //le nombre de reservation sera enregistre
    const [flagEdit,SetFlagEdit]=useState("0");
    //gestioner l'ecran modal le parametre optionReservation et idReservation sont facultatifs
    //il est utilise pour reutiliser l'ecran modal
    const toggleModal = (option,optionReservation=0,idReservacion="0") =>{
        //gestioner l'ecran modal selon l'option
        if(option===1)
            setModal(!modal);
        if(option===2)
            setModalLogin(!modalLogin);
        if(option===3)
        {
            if(optionReservation!==undefined)//il est une modification
                SetFlagEdit(idReservacion);//ajouter le idReservation pour le modifier
            else
                SetFlagEdit("0");//il est une creation de compte
            if(!modalReservation)
                setMessageModal("");
            setModalReservation(!modalReservation);

        }
            
    } 
    //gestioner les message de validation
    const [messageModal,setMessageModal]=useState("");
    const[messageTab,setMessageTab]=useState("");
    //gestioner le button de creation 
    const[enableButton,setEnableButton]=useState(true);
    //courriel et mot de passe pour s'inscrire 
    const [form,setForm]=useState({
        email:'',
        password:''
    });
    //valider l'acces avec le courriel et le mot de passe
    const loginUser= (props,e) => {
        e.nativeEvent.stopImmediatePropagation();
        //obtenir les donnees
        const email=form.email;  
        //crypter le mote de passe
        const password=sha256(form.password);
        //transformer les donnes au json avec le web api
        const client = ClienteDto({ email,password });
        //faire le demande au web service et gestioner le promise comme un resultat
        postData(constant.API_LOGIN,client)
        .then((response)=>{
            console.log(response.data);
            if(response.data===1)
            {    
                //si la validation est correcte faire une redirection au ecran de despoibilite            
                setUserName(email);
                props.history.push(constant.REACT_ROUTER_AVAILABILITY);
            }
            else
            {
                setMessageTab(messages.COMPTE_ERROR_LOGIN);
            }
                
            setModalLogin(!modalLogin);
        })
        .catch((error)=>{
            if (error.response) {
                //le web api returne un 404 quand le courriel ou le mot de passe ne sont pas correctes
                if(error.response.status===404)
                    setMessageTab(messages.COMPTE_ERROR_LOGIN);
                //erreur de connection 
                else
                    setMessageTab(messages.COMPTE_ERROR_LOGIN_COMMUNICATION);
              }
            else
                //erreur de connection
                setMessageTab(messages.COMPTE_ERROR_LOGIN_COMMUNICATION);
            
            setModalLogin(!modalLogin);
        });

    }
    //faire une demande au web service pour creation de compte
    const CreateUser=(props, e)=>{
        e.nativeEvent.stopImmediatePropagation();
        const email=form.email;
        //crypter le mot de passe avec sha256
        const password=sha256(form.password);
        //transformer les donnes au json avec le web api
        const client = ClienteDto({ email,password });
        //faire le demande au web service et gestioner le promise comme un resultat
        postData("createUser",client)
        .then((response)=> {
              // si le promise est correcte validar la creation
              if(response.data===1)
                setMessageTab(messages.COMPTE_CREATION_SUCCESS)
              else
                  setMessageTab(messages.COMPTE_CREATION_ERROR);
            //fermer le modal
            setModal(false);
        })
        .catch((error)=>{
            //valider le response de erreur
            if (error.response) {
                //le web api returne un 404 quand le courriel deja existe
                if(error.response.status===404)
                    setMessageTab(messages.COMPTE_CREATION_ALREADY_EXISTS);
                //erreur de connection 
                else
                    setMessageTab(messages.COMPTE_CREATION_ERROR);
              }
            else
                //erreur de connection
                setMessageTab(messages.COMPTE_CREATION_ERROR);
            setModal(false);
        });   
    }
    const handleChange = (e,field) => {
        let emailTmp = "";
        let passwordTmp = "";
        //valider le text qui est modifie pour l'utilisateur
        if (field === 'email') {
            emailTmp = e.target.value;
            passwordTmp = form.password;
        }
        else {
            emailTmp = form.email;
            passwordTmp = e.target.value;
        }
        //changer l'etat et pour callback faire la validation
        setForm({
            email: emailTmp,
            password: passwordTmp
        },validateFields(emailTmp,passwordTmp));
    }

    //validate le courriel format
    const validateFields = (prEmail, prPassword) => {
        if (validator.isEmail(prEmail)) {
            setMessageModal("");
            //si le courriel etait correct donc valider le mot de passe
            validatePasword(prPassword);
        }
        else {
            setMessageModal(messages.COMPTE_CREATION_EMAIL_FORMAT_ERROR);
            setEnableButton(true);
        }
    }
    //valider que le mot de passe n'est pas vide
    const validatePasword = (prPassword) => {
        if (prPassword !== "") {
            setMessageModal("");
            setEnableButton(false);
        }
        else {
            setMessageModal(messages.COMPTE_CREATION_PASSWORD_EMPTY);
            setEnableButton(true);
        }
    }

    //exporter les etats et les fonctions avec les autres components
    const data={userName,modalReservation,modal,form,messageModal,enableButton,messageTab,modalLogin
        ,loginUser,flagEdit,toogleModalLogin,setEnableButton,setMessageModal
        ,toggleModal,CreateUser,setForm,handleChange}
    return(
        <ModalContext.Provider value={data}>
            {children}
        </ModalContext.Provider>
    )
}
export default ModalContext;
export {ModalProvider}