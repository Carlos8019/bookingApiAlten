import validator from 'validator';
import { useContext } from 'react';
import { Modal, ModalHeader, ModalBody, ModalFooter, Button, Input, Label } from 'reactstrap';
import ModalContext from '../Contexts/ModalContext';
export default function CreateUser(props) {
    //importer le Context pour utiliser le modal
    const { form, toggleModal, modal, enableButton, setEnableButton, messageModal, setForm, ValiderLogin, setMessageModal } = useContext(ModalContext);

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
        console.log("validate",prEmail," ", prPassword);
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
        console.log("password", prPassword, props.form);
        if (prPassword !== "") {
            setMessageModal("");
            setEnableButton(false);
        }
        else {
            setMessageModal("Le mot de passe est vide");
            setEnableButton(true);
        }
    }
    return (
        <div>
            <Modal isOpen={modal} toggle={toggleModal}>
                <ModalHeader toggle={toggleModal}>Créer un compte</ModalHeader>
                <ModalBody>
                    <Label>Courriel</Label>
                    <Input
                        placeholder="votre_compte@fornisseur.com"
                        required
                        type="text"
                        name="email"
                        onChange={(e) => handleChange(e,"email")}
                    />

                    <Label>Mot de passe</Label>
                    <Input
                        placeholder="votre mote de passe"
                        required
                        name="password"
                        type="password"
                        onChange={(e) => handleChange(e,"password")}
                    />
                    <p>{messageModal}</p>

                </ModalBody>
                <ModalFooter>
                    <Button disabled={enableButton} color="primary" onClick={(e) => ValiderLogin(props, e)}>Enregistrer</Button>{' '}
                    <Button color="secondary" onClick={toggleModal}>Cancel</Button>
                </ModalFooter>
            </Modal>
        </div>

    )
}
