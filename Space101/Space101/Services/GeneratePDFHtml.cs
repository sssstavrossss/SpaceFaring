using Space101.Models;
using Space101.ViewModels.OrderViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Space101.Services
{
    public static class GeneratePDFHtml
    {

        public static string ReturnHtml(PrintOrderViewModel Model)
        {
            return $@"
                        <div class='row order-row'>
                        <div class='col-xs-12'>
                            <div class='row order-header'>
                                <div class='col-xs-12 header-details'>
                                    <div  style='letter-spacing: 5px;'>
                                            <h1  style='letter-spacing: 5px;'>Space Faring</h2>
                                        </div>
                                    <div>
                                        <h1 style='letter-spacing: 5px;' > Order Details</h1>
                                    </div>
                                </div>
                            </div>
                            <div class='row order-header'>
                                <div class='col-xs-6 header-details'>
                                    <div>
                                        <h5>
                                            <img src='/images/payment-method.png' /> Order Number: {Model.OrderId}
                                          </h5>
                                    </div>
                                    <div class='header-details-info'>
                                        <h5>
                                            <img src='/images/attach.png' /> {Model.InvoiceId}
                                           </h5>
                                    </div>
                                </div>
                                <div class='col-xs-6  header-details'>
                                    <div>
                                        <h5>
                                            <img src='/images/timetable.png' /> Order Date: {Model.CreationDate}
                                           </h5>
                                    </div>
                                    <div>
                                        <h5>
                                            <img src='/images/email.png' /> Buyer Email: {Model.BuyerEmail}
                                          </h5>
                                    </div>
                                </div>
                            </div>
                            <div class='row order-details-row'>
                                <div class='col-xs-5 order-details-div'>
                                    <h5>Flight Information</h5>
                                    <div class='order-detail-box'>
                                        <div style='padding: 8px 10px 5px; background-color: rgba(255,255,255,.08); border-radius: 20px; margin-left: -3px;'>
                                            <img src='/images/lastname.png' style='height: 20px; width: auto; margin-top: -8px;'/> &nbsp; Flight ID: &nbsp; {Model.FlightId}
                                           </div>
                                    </div>
                                    <div class='order-detail-box'>
                                            <div style='padding: 8px 10px 5px; background-color: rgba(255,255,255,.08); border-radius: 20px; margin-left: -3px;'>
                                            <img src='/images/timetable.png' style='height: 20px; width: auto; margin-top: -8px;'/> &nbsp; Flight Date: &nbsp; {Model.Tickets.First().FlightDate}
                                           </div>
                                    </div>
                                    <div class='order-detail-box'>
                                            <div style='padding: 8px 10px 5px; background-color: rgba(255,255,255,.08); border-radius: 20px; margin-left: -3px;'>
                                                <img src='/images/spaceship.png' style='height: 20px; width: auto; margin-top: -8px;'/> &nbsp; Starship: &nbsp; {Model.Tickets.First().StarshipModel}
                                           </div>
                                    </div>
                                    <div class='order-detail-box'>
                                            <div style='padding: 8px 10px 5px; background-color: rgba(255,255,255,.08); border-radius: 20px; margin-left: -3px;'>
                                                <img src='/images/tickets.png' style='height: 20px; width: auto; margin-top: -8px;'/> &nbsp; Tickets Total &nbsp; {Model.Tickets.Count}
                                           </div>
                                    </div>
                                </div>
                                <div class='col-xs-5 order-details-div'>
                                    <h5>Ticket Information: </h5> {OrderTicket(Model)}
                                </div>
                                <div class='col-xs-2 order-totals'>
                                    <div class='total-price-box'>
                                        <div>
                                            <div style='display: flex; background-color: rgba(255,255,255,.33); margin-bottom: 0px; border-radius: 15px 15px 0px 0px; padding-bottom: 7px;' >
                                                <span><img style='margin-top: 6px; margin-left: 6px;' src='/images/dollar.png' /></span>
                                                <h4 style = 'margin-left: 3px;  font-size: 13px;' > Total Price</h4>
                                            </div>
                                            <h4 style='background-color: orangered; border-radius: 0px 0px 15px 15px; padding: 5px 3px; margin-bottom: 0px; margin-top: 0px;' > &nbsp; {Model.TotalPrice}&#36;</h4>
                                        </div>
                                    </div>
                                    <div class='payment-method-box'>
                                        <div>
                                            <div style = 'display: flex; background-color: rgba(255,255,255,.33); margin-bottom: 0px; border-radius: 15px 15px 0px 0px;'>
                                                <span>
                                                    <img style='margin-top: 11px; margin-left: 9px;' src='/images/payment-method.png' />
                                                </span>
                                                <h4 style='margin-left: -5px; margin-top: -2px; font-size: 14px;' > Payment Method: </h4>
                                            </div>
                                            <h4 style='background-color: coral; border-radius: 0px 0px 15px 15px; padding: 5px 0px; margin-bottom: 0px; margin-top: -11px;' > &nbsp; Paypal</h4>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div><br/><br/>{Ticket(Model)}";
        }

        private static string OrderTicket(PrintOrderViewModel Model)
        {
            string ret = "";
            foreach (var ticket in Model.Tickets)
            {
                ret += $@"<div class='order-detail-box'>
<div style='padding: 8px 10px 5px; background-color: rgba(255,255,255,.08); border-radius: 20px; margin-left: -3px;'>
                                                <img src='/images/tickets.png' style='height: 20px; width: auto; margin-top: -8px;'/> &nbsp; Ticket Number &nbsp; {ticket.Price} &nbsp; {ticket.TicketId}
                                           </div>
                            </div>";
            }
            return ret;
        }

        private static string Ticket(PrintOrderViewModel Model)
        {
            string ret = "";
            foreach (var ticket in Model.Tickets)
            {
                                    ret +=  $@"<div class='row flight-row'>
                            <div class='col-xs-4'>
                                <div class='flight-div'>
                                    <div class='flight-details'>
                                        <div class='flight-details-departure'>
                                            <img src = '/images/firstname.png' alt='planet logo' style='height: 30px; width: auto;' />
                                            First Name: 
                                            {ticket.FirstName}
                                        </div>
                                        <div class='flight-details-departure'>
                                            <img src = '/images/internet.png' alt='planet logo' style='height: 30px; width: auto;' />
                                            Departure: 
                                            {ticket.DeparturePlanetName}
                                        </div>
                                        <div class='flight-details-destination'>
                                            <img src = '/images/internet.png' alt='planet logo' style='height: 30px; width: auto;' />
                                            Destination: 
                                            {ticket.DestinationPlanetName}
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class='col-xs-4'>
                                <div class='flight-div'>
                                    <div class='flight-details'>
                                        <div class='flight-details-date'>
                                            <img src = '/images/lastname.png' alt='date logo' style='height: 30px; width: auto;' />
                                            Last Name: 
                                            {ticket.LastName}
                                        </div>
                                        <div class='flight-details-date'>
                                            <img src = '/images/timetable.png' alt='date logo' style='height: 30px; width: auto;' />
                                            {ticket.FlightDate}
                                        </div>
                                        <div class='flight-details-price'>
                                            <img src ='/images/dollar.png' alt='dollar logo' style='height: 30px; width: auto;' />
                                            Price: 
                                            {ticket.Price}
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class='col-xs-3'>
                                <div class='col-xs-12 starship-div'>
                                    <div class='col-xs-12 starship-details'>
                                        <div class='col-xs-1 starship-details-intro' style='padding: 0px; margin: 0px;'>
                                            <div><img src = '/images/spaceship.png' alt='spaceship logo' style='height: 38px; width: auto; margin-top: 30px;' /></div>
                                        </div>
                                        <div class='gamisemas col-xs-11' style='font-size: 18px; margin-top: 8px;'>
                                            <div class='col-xs-12 starship-details-name'  style='font-size: 15px; margin-letf: -5px;'>
                                                {ticket.StarshipModel}
                                            </div>
                                            <div class='col-xs-12 starship-details-passengers'  style='font-size: 15px; margin-letf: -5px;'>
                                                Ticket: {ticket.TicketId}
                                            </div>
                                            <div class='col-xs-12 starship-details-passengers'  style='font-size: 15px; margin-letf: -5px;'>
                                                Seat: {ticket.FlightSeatId}
                                            </div>
                                            <div class='col-xs-12 starship-details-travelclass'  style='font-size: 15px; margin-letf: -5px;'>
                                                Class: {ticket.TravelClassId}
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class='col-xs-1 super-is-vip vip-super-div-inflight' style='margin-left: -7px;'>
                                <div class='is-vip vip-div-inflight'>{returnNo(Model)} VIP</div>
                    </div>
                    </div>";
            }
            return ret;
        }

        private static string returnNo(PrintOrderViewModel Model)
        {
            string ret = "";
            if (Model.isVip)
                ret = "No <br/>";
            return ret;
        }
    }
}