import { useContext, useState } from 'react';
//Validator permets faire des validations en ce cas pour le courriel

import { TabContent, TabPane, Nav, NavItem, NavLink, Card, Button, CardTitle, CardText, Row, Col, CardBody } from 'reactstrap';
import classnames from 'classnames';
import ModalContext from '../Contexts/ModalContext';
import Login from './Login';
import CheckRooms from './CheckRooms';
import CreateUser from './CreateUser';
/*Cette fontion implement l'écran principal avec la parti de login et sign-up
afin de seconecter et verifier le disponibilite
j'utilise le react-strap et le bootstrap pour developper les écrans, le redux pour 
gestioner les donnees et les hooks du react pour l'interaction les components
*/
function App() {
  //utiliser l'usestate pour gestioner le Tab component
  const [activeTab, setActiveTab] = useState('1');
  const toggle = tab => {
    if (activeTab !== tab) setActiveTab(tab);
  }
  //importer le Context pour utiliser le modal
  const { toggleModal, messageTab } = useContext(ModalContext);
  //creation du Tab pour aborder a le signup ou le signin et verifier la disponibilite
  return (
    <>
      <div class="container">
        <div class="row h-50">
          <div class="col-sm-6 h-100 d-table">
            <div class="card card-body d-table-cell align-middle">
              Bookin API Alten Challenge
            </div>
          </div>
        </div>
      </div>
      <br/>
      <div className="d-flex align-items-center justify-content-center">
        <div className="container">
          <Nav tabs>
            <NavItem>
              <NavLink
                className={classnames({ active: activeTab === '1' })}
                onClick={() => { toggle('1'); }}
              >
                Vérifier la disponibilité
              </NavLink>
            </NavItem>
            <NavItem>
              <NavLink
                className={classnames({ active: activeTab === '2' })}
                onClick={() => { toggle('2'); }}
              >
                Information du Challenge Alten
              </NavLink>
            </NavItem>
          </Nav>
          <TabContent activeTab={activeTab}>
            <TabPane tabId="1">
              <Row>
                <Col sm="6">
                  <Card body>
                    <CardTitle>S'inscrire</CardTitle>
                    <CardText>Créez votre compte et vérifiez votre réservation et la disponibilité des chambres.</CardText>
                    <Button onClick={() => toggleModal(1)}>Sign-Up</Button>
                  </Card>
                </Col>
                <Col sm="6">
                  <Card body>
                    <CardTitle>Se Connecter</CardTitle>
                    <CardText>Connectez-vous à votre compte et vérifiez votre réservation et la disponibilité des chambres..</CardText>
                    <Button onClick={() => toggleModal(2)}>Sign-In</Button>
                  </Card>
                </Col>
              </Row>
              <Row>
                <Col>
                {messageTab ? <p className="alert alert-info">{messageTab}</p> : <p></p>}
                <Card body>
                  <CardTitle>disponibilite</CardTitle>
                  <CardText>Résumé de la disponibilité des chambres</CardText>
                  <CardBody>
                    <CheckRooms />
                  </CardBody>
                </Card>
                </Col>
              </Row>

            </TabPane>
            <TabPane tabId="2">
              <Row>
                <Col sm="12">
                  <Card body>
                    <CardText><h6>Alten Challenge</h6><br /><h6>Developed by Carlos Yanez</h6>
                      <br /><a href="https://github.com/Carlos8019/bookingApiAlten">GitHub Projet</a>
                      <p>CHALLENGE
                          Post-Covid scenario:
                          People are now free to travel everywhere but because of the pandemic, a lot of hotels went
                          bankrupt. Some former famous travel places are left with only one hotel.
                          You’ve been given the responsibility to develop a booking API for the very last hotel in Cancun.</p>
                      
                      </CardText>
                  </Card>
                </Col>
              </Row>
            </TabPane>
          </TabContent>
        </div>

        <Login />
        <CreateUser />
      </div>
    </>
  );
}

export default App;

/* <CreateUser /> */

/*

              <h3>Problematique</h3>
              <p>CHALLENGE</p>
              <p>Post-Covid scenario:</p>
              <p>People are now free to travel everywhere but because of the pandemic, a lot of hotels went
                bankrupt. Some former famous travel places are left with only one hotel.
                You’ve been given the responsibility to develop a booking API for the very last hotel in Cancun.</p>
              <p>The requirements are:</p>
              <p>- API will be maintained by the hotel’s IT department.</p>
              <p>- As it’s the very last hotel, the quality of service must be 99.99 to 100%  no downtime</p>
              <p>- For the purpose of the test, we assume the hotel has only one room available</p>
              <p>- To give a chance to everyone to book the room, the stay can’t be longer than 3 days and
              can’t be reserved more than 30 days in advance.</p>
              <p>- All reservations start at least the next day of booking,</p>
              <p>- To simplify the use case, a “DAY’ in the hotel room starts from 00:00 to 23:59:59.</p>
              <p>- Every end-user can check the room availability, place a reservation, cancel it or modify it.</p>
              <p>- To simplify the API is insecure.</p>
              <p>Instructions :</p>
              <p>- Pas de limite de temps (très bien fait il faut au moins 3 à 4 soirées)</p>
              <p>- Le minimum requis est un README et du code.</p>
              <p>- Tous les shortcuts pour gagner du temps sont autorisés dans la mesure où c’est
                documenté. Tout shortcut non expliqué doit etre consideré comme une erreur. On
                pourrait accepter un rendu avec 3 lignes de code si elles ont du sens et que tout le
                raisonnement et les problèmatiques à prendre en compte sont decrites.
              </p>


              */