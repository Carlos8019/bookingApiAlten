import { createContext, useState } from "react";
import validator from 'validator';
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
    //gestioner l'ecran modal du login
    const[modalLogin,setModalLogin]=useState(false);
    const toogleModalLogin=()=>setModalLogin(!modal);
    //gesioner l'ecran modal de la reservation
    const [modalReservation,setModalReservation]=useState(false);

    const toggleModal = (option) =>{
        console.log(option);
        if(option===1)
            setModal(!modal);
        if(option===2)
            setModalLogin(!modalLogin);
        if(option===3)
            setModalReservation(!modalReservation);
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
        const password=sha256(form.password);
        const client = ClienteDto({ email,password });
        //console.log(props.history);
        props.history.push("/disponibilite");
        //faire le demande au web service et gestioner le promise comme un resultat
        postData("login",client)
        .then((response)=>{
            console.log(response.data);
            if(response.data===1)
                props.history.push("/disponibilite");
            else
                setMessageTab("Erreur de création de compte, veuillez réessayer.");
            setModalLogin(!modalLogin);
        })
        .catch((error)=>{
            
            if (error.response) {
                //le web api returne un 404 quand le courriel ou le mot de passe ne sont pas correctes
                if(error.response.status===404)
                    setMessageTab("Courriel ou mot de passe incorrect, veuillez réessayer.");
                //erreur de connection 
                else
                    setMessageTab("Erreur de création de compte, veuillez réessayer.");
              }
            else
                //erreur de connection
                setMessageTab("Erreur de création de compte, veuillez réessayer.");
            
            setModalLogin(!modalLogin);
        });

    }
    //faire une demande au web service et valider l'acces
    const CreateUser=(props, e)=>{
        e.nativeEvent.stopImmediatePropagation();
        const email=form.email;
        //crypter le mot de passe avec sha256
        const password=sha256(form.password);
        const client = ClienteDto({ email,password });
        //faire le demande au web service et gestioner le promise comme un resultat
        postData("createUser",client)
        .then((response)=> {
              // si le promise est correcte validar la creation
              if(response.data===1)
                setMessageTab("Le Compte a été créé avec succès, veuillez vous connecter pour y accéder.")
              else
                  setMessageTab("Erreur de création de compte, veuillez réessayer.");
            //fermer le modal
            setModal(false);
        })
        .catch((error)=>{
            //valider le response de erreur
            if (error.response) {
                //le web api returne un 404 quand le courriel deja existe
                if(error.response.status===404)
                    setMessageTab("Le compte que vous avez saisi existe déjà, veuillez vous connecter.");
                //erreur de connection 
                else
                    setMessageTab("Erreur de création de compte, veuillez réessayer.");
              }
            else
                //erreur de connection
                setMessageTab("Erreur de création de compte, veuillez réessayer.");
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
            setMessageModal("Format de courrier incorrect");
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
            setMessageModal("Le mot de passe est vide");
            setEnableButton(true);
        }
    }

    //exporter les etast et les fonctions avec les autres components
    const data={modalReservation,modal,form,messageModal,enableButton,messageTab,modalLogin
        ,loginUser,toogleModalLogin,setEnableButton,setMessageModal
        ,toggleModal,CreateUser,setForm,handleChange}
    return(
        <ModalContext.Provider value={data}>
            {children}
        </ModalContext.Provider>
    )
}
export default ModalContext;
export {ModalProvider}