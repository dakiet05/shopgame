﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using xuonggame.Models;

namespace xuonggame.Controllers
{
    public class ShoppingCartController : Controller
    {
        private XUONGGAME_DBEntities1 dBContext = new XUONGGAME_DBEntities1();
        private string strCart = "Cart";
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OrderNow(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session[strCart] == null)
            {
                List<Cart> ListCart = new List<Cart>
                {
                    new Cart(dBContext.Products.Find(Id),1)
                };
                Session[strCart] = ListCart;
            }
            else
            {
                List<Cart> ListCart = (List<Cart>)Session[strCart];
                int check = IsExistingCheck(Id);
                if (check == -1)
                    ListCart.Add(new Cart(dBContext.Products.Find(Id), 1));
                else
                    ListCart[check].Quantity++;
                Session[strCart] = ListCart;
            }
            return RedirectToAction("Index");
        }
        private int IsExistingCheck(int? Id)
        {
            List<Cart> ListCart = (List<Cart>)Session[strCart];
            for (int i = 0; i < ListCart.Count; i++)
            {
                if (ListCart[i].Product.ProId == Id)
                    return i;
            }
            return -1;
        }
        public ActionResult RemoveItem(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int check = IsExistingCheck(Id);
            List<Cart> ListCart = (List<Cart>)Session[strCart];
            ListCart.RemoveAt(check);
            if (ListCart.Count == 0)
            {
                Session[strCart] = null;
            }
            else
            {
                Session[strCart] = ListCart;
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult UpdateCart(FormCollection field)
        {
            if (Session[strCart] != null)
            {
                List<Cart> ListCart = (List<Cart>)Session[strCart];

                string[] quantities = field.GetValues("quantity");

                for (int i = 0; i < ListCart.Count && i < quantities.Length; i++)
                {
                    int newQuantity;
                    if (int.TryParse(quantities[i], out newQuantity))
                    {
                        // Kiểm tra và giới hạn số lượng không âm
                        newQuantity = Math.Max(newQuantity, 0);

                        // Cập nhật số lượng sản phẩm
                        ListCart[i].Quantity = newQuantity;

                        // Xóa sản phẩm nếu số lượng là 0
                        if (newQuantity == 0)
                        {
                            ListCart.RemoveAt(i);
                            i--; // Điều chỉnh chỉ số sau khi xóa phần tử
                        }
                    }
                    else
                    {
                        Session.Remove(strCart);
                    }
                }

                // Cập nhật lại session
                Session[strCart] = ListCart;
            }

            return RedirectToAction("Index");
        }

        public ActionResult ClearCart()
        {
            Session[strCart] = null;
            return RedirectToAction("Index");
        }
        public ActionResult CheckOut()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ProcessOder(FormCollection field)
        {
            List<Cart> ListCart = (List<Cart>)Session[strCart];
            var order = new xuonggame.Models.Order()
            {
                CustomerName = field["cusName"],
                CustomerPhone = field["cusPhone"],
                CustomerEmail = field["cusEmail"],
                CustomerAddress = field["cusAddress"],
                OrderDate = DateTime.Now,
                PaymentType = "Cash",
                Status = "Processing"
            };
            dBContext.Orders.Add(order);
            dBContext.SaveChanges();
            foreach (Cart cart in ListCart)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    OrderId = order.OrderId,
                    ProductId = cart.Product.ProId,
                    Quantity = Convert.ToInt32(cart.Quantity),
                    Price = Convert.ToDouble(cart.Product.ProPrice)
                };
                dBContext.OrderDetails.Add(orderDetail);
                dBContext.SaveChanges();

            }
            Session.Remove(strCart);
            return View("ProcessOder");
        }
    }
}