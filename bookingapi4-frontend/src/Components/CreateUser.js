
import { useContext } from 'react';
import { Modal, ModalHeader, ModalBody, ModalFooter, Button, Input, Label } from 'reactstrap';
//context d'ecran modal
import ModalContext from '../Contexts/ModalContext';
export default function CreateUser(props) {
    //importer le Context pour utiliser le modal
    const { toggleModal,handleChange, modal, enableButton, messageModal,  CreateUser } = useContext(ModalContext);
    return (
        <div>
            <Modal isOpen={modal} toggle={()=>toggleModal(1)}>
                <ModalHeader toggle={()=>toggleModal(1)}>Créer un compte</ModalHeader>
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
                    <Button disabled={enableButton} color="primary" onClick={(e) => CreateUser(props, e)}>Enregistrer</Button>{' '}
                    <Button color="secondary" onClick={()=>toggleModal(1)}>Annuler</Button>
                </ModalFooter>
            </Modal>
        </div>
    )
}
