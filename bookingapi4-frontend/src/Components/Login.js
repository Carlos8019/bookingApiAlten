import React, { useContext } from 'react'
import { Modal, ModalHeader, ModalBody, ModalFooter, Button, Input, Label } from 'reactstrap';
import ModalContext from '../Contexts/ModalContext'
/*
Cette fonction permet se connecter en utilisant le web service pour valider le courriel
et le mot de passe
 */
function Login(props) {
    //utiliser le Modal Context pour gestioner les fonctions
    const {modalLogin,toggleModal,handleChange,messageModal,enableButton,loginUser}=useContext(ModalContext);

    return (
        <div>
            <Modal isOpen={modalLogin} toggle={()=>toggleModal(2)}>
                <ModalHeader toggle={()=>toggleModal(2)}>Se Connecter</ModalHeader>
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
                    <Button disabled={enableButton} color="primary" onClick={(e) => loginUser(props,e)}>Se Connecter</Button>{' '}
                    <Button color="secondary" onClick={()=>toggleModal(2)}>Annuler</Button>
                </ModalFooter>
            </Modal>
        </div>

   )
}

export default Login
