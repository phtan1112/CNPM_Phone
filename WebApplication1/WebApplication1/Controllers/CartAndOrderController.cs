using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CartAndOrderController : Controller
    {
        private MyConn db = new MyConn();

        private string cart = "Cart";

        // GET: CartAndOrder
        //load all phones that agent add to cart of this agent (load phone added into cart)
        public ActionResult Index()
        {
            if (Session["username"] != null)
            {
                return View();
            }
            else
                {
                    return Redirect("/User");
                }
            
        }

        // when click  "add to cart" all of these phone will add into cart and show all phones
        public ActionResult OrderNow(string id)
        {
           
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            if (Session[cart] == null)
            {
                List<Cart> carts = new List<Cart> { new Cart(
                    db.phones.Find(Int32.Parse(id)),1) };

                Session[cart] = carts;
            }
            else
            {
                List<Cart> carts = (List<Cart>)Session[cart];

                int indexAt = IsExisting(id);

                if (indexAt == -1)
                {
                    carts.Add(new Cart(db.phones.Find(Int32.Parse(id)), 1));
                }
                else
                {
                    carts[indexAt].Quantity++;
                }

                Session[cart] = carts;
            }

            return RedirectToAction("Index");
        }

        //check this phone is going to add that exist in cart or not, if yes, will plus quantity
        private int IsExisting(string id)
        {
            List<Cart> carts = (List<Cart>)Session[cart];

            for (int i = 0; i < carts.Count; i++)
            {
                if (carts[i].phone.id.ToString() == id)
                {
                    return i;
                }
            }

            return -1;
        }

        //delephone from cart
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            int indexAt = IsExisting(id);

            List<Cart> carts = (List<Cart>)Session[cart];

            carts.RemoveAt(indexAt);

            if (carts.Count == 0)
            {
                Session[cart] = null;
            }
            else
            {
                Session[cart] = carts;
            }

            return RedirectToAction("Index");
        }

        //update quantity when agent change quantity in cart
        public ActionResult UpdateOrder(FormCollection formCollection)
        {
            string[] quantities = formCollection.GetValues("quantity");

            List<Cart> carts = (List<Cart>)Session[cart];

            for (int i = 0; i < carts.Count; i++)
            {
                carts[i].Quantity = Convert.ToInt32(quantities[i]);
            }

            Session[cart] = carts;

            return RedirectToAction("Index");
        }

        //agent click checkout button so will be displayed the ProgressingCash to choose the method pay
        public ActionResult ProcessingCash()
        {
           
             if (Session["username"] != null)
            {
                return View();
            }
            else
            {
                return Redirect("/User");
            }
        }

        //after choose method pay and click "Done" button so all phones in cart will order and add into database
        public ActionResult Cash(FormCollection formCollection)
        {
            List<Cart> carts = (List<Cart>)Session[cart];

            // save agent order
            int totalPrice = (int)carts.Sum(c => c.Quantity * c.phone.price);

            DateTime now = DateTime.Now;
            string agentId = Session["agentID"].ToString();
            string methdPay = formCollection["pay"];

            agent_order order = new agent_order()
            {
                agent_id = Int32.Parse(agentId),
                order_date = now,
                total = totalPrice,
                status_order = false,
                status_pay = false,
                method_pay = methdPay
            };

            db.agent_order.Add(order);
            db.SaveChanges();




            // save order detail receipt
            for (int i = 0; i < carts.Count; i++)
            {
                agent_order_detail detail_order = new agent_order_detail()
                {
                    order_id = order.id,
                    id_phone = carts[i].phone.id,
                    quantity = carts[i].Quantity

                };

                db.agent_order_detail.Add(detail_order);
                db.SaveChanges();
            }

            Session.Remove(cart);

            return RedirectToAction("Orders");
        }

        //load all orders of agent into view
        public ActionResult Orders()
        {
            if (Session["username"] != null)
            {
                string ID_of_agent = Session["agentID"].ToString();

                var orders = db.agent_order.Where(x => x.agent_id.ToString() == ID_of_agent).ToList();

                return View(orders);
            }
            else
            {
                return Redirect("/User");
            }
           
        }

        //click "see detail" on each order of agent so that show all phones of order clicked
        public ActionResult OrderDetail(int id)
        {
            if (id.ToString() == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            if (id.ToString() != null && Session["username"] != null)
            {
                var orderDetail = db.agent_order_detail.Where(o => o.order_id == id).ToList();

                ViewBag.Phones = db.phones.ToList();

                return View(orderDetail);
            }
            else
            {
                return Redirect("/User");
            }
            
        }
    }
}
