import {useContext,Fragment} from 'react';
import { Modal, ModalHeader, ModalBody, ModalFooter, Label } from 'reactstrap';
import Box from '@material-ui/core/Box';
import AdapterDateFns from '@material-ui/lab/AdapterDateFns';
import LocalizationProvider from '@material-ui/lab/LocalizationProvider';
import DateRangePicker from '@material-ui/lab/DateRangePicker';
import { Button } from '@material-ui/core';
import ModalContext from '../Contexts/ModalContext';
import ReservationContext from '../Contexts/ReservationContext';

function Reservation(props) {
  const {modalReservation,toggleModal,messageModal,enableButton}=useContext(ModalContext);
  const {CreateReservation,valueDates, handleChangeDates}=useContext(ReservationContext);
  return (
    <div>
      <Modal isOpen={modalReservation} toggle={() => toggleModal(3)}>
        <ModalHeader toggle={() => toggleModal(3)}>Réservation</ModalHeader>
        <ModalBody>
          <Label>Sélectionnez les jours de votre réservation</Label>
          <LocalizationProvider dateAdapter={AdapterDateFns}>
            <DateRangePicker
              label="Advanced keyboard"
              value={valueDates}
              onChange={(newValue) =>handleChangeDates(newValue) }
              renderInput={(startProps, endProps) => (
                <Fragment>
                  <input ref={startProps.inputRef} {...startProps.inputProps} />
                  <Box sx={{ mx: 1 }}> to </Box>
                  <input ref={endProps.inputRef} {...endProps.inputProps} />
                </Fragment>
              )}
            />
          </LocalizationProvider>
          <p>{messageModal}</p>
        </ModalBody>
        <ModalFooter>
          <Button  variant="contained" disabled={enableButton} color="primary" onClick={(e) => CreateReservation(props, e)}>Réserver</Button>{' '}
          <Button color="secondary" onClick={() => toggleModal(3)}>Annuler</Button>
        </ModalFooter>
      </Modal>
    </div>
  );
}
export default Reservation;