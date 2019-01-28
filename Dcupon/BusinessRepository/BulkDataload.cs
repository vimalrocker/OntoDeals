using Dcupon.Configuration;
using Dcupon.DAL;
using Dcupon.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Dcupon.BusinessRepository
{
    public class BulkDataload
    {
        bool statusaction;
        bool apiuploadstatus;
        CuponDetailsModel couponmodel = new CuponDetailsModel();
        List<CuponDetail> listcupondetails_groupon = new List<CuponDetail>();
        List<object> finaldata = new List<object>();
       // public static var Num { get; set; }
        public bool bulkSave(JObject json)
        {
            try
            {

                var tableRowshotel = (from p in json["accommodation"]
                                      select new
                                      {
                                          subcatagoryitem = "Hotel",
                                          // title = "Upto 50% Off On All Travel Deals",
                                          description = "",
                                          discount = (int)p["discountPercent"],
                                          category = "0",
                                          promocode = "0",
                                          Url = "http://t.groupon.co.in/r?tsToken=IN_AFF_0_205459_516_0&url=http%3A%2F%2Fgetaways.groupon.co.in%2Fcoupons%2Fcity-hotels%3FCID%3DIN_AFF_5600_225_5383_1%26nlp%26utm_medium%3Dafl%26utm_source%3DGPN%26utm_campaign%3D205459%26mediaId%3D516",
                                          merchantName = (string)p["merchantName"]
                                      }).OrderByDescending(c => c.discount).FirstOrDefault();
                finaldata.Add(tableRowshotel);

                var tableRowstour = (from p in json["excursion"]
                                     select new
                                     {
                                         subcatagoryitem = "Tour",
                                         description = "",
                                         discount = (int)p["discountPercent"],
                                         category = "0",
                                         promocode = "0",
                                         Url = "http://t.groupon.co.in/r?tsToken=IN_AFF_0_205459_516_0&url=http%3A%2F%2Fgetaways.groupon.co.in%2Fcoupons%2Ftour-packages%3FCID%3DIN_AFF_5600_225_5383_1%26nlp%26utm_medium%3Dafl%26utm_source%3DGPN%26utm_campaign%3D205459%26mediaId%3D516",
                                         merchantName = (string)p["merchantName"]
                                     }).OrderByDescending(c => c.discount).FirstOrDefault();

                finaldata.Add(tableRowstour);

                var tableRowsDayouting = (from p in json["leisure"]
                                     select new
                                     {
                                         subcatagoryitem = "leisure",
                                         description = "",
                                         discount = (int)p["discountPercent"],
                                         category = "0",
                                         promocode = "0",
                                         Url = "http://t.groupon.co.in/r?tsToken=IN_AFF_0_205459_516_0&url=http%3A%2F%2Fgetaways.groupon.co.in%2Fcoupons%2Fgetaways-and-day-outings%3FCID%3DIN_AFF_5600_225_5383_1%26nlp%26utm_medium%3Dafl%26utm_source%3DGPN%26utm_campaign%3D205459%26mediaId%3D516",
                                         merchantName = (string)p["merchantName"]
                                     }).OrderByDescending(c => c.discount).FirstOrDefault();

                finaldata.Add(tableRowsDayouting);






                var tablerowshealth = (from p in json["healthcare"]
                                       select new
                                       {
                                           subcatagoryitem = "Health",
                                           description = "",
                                           discount = (int)p["discountPercent"],
                                           category = "0",
                                           promocode = "0",
                                           Url = "http://t.groupon.co.in/r?tsToken=IN_AFF_0_205459_516_0&url=http%3A%2F%2Fproducts.groupon.co.in%2Fshopping%2Fhealth-care%2F%3FCID%3DIN_AFF_5600_225_5383_1%26nlp%26utm_medium%3Dafl%26utm_source%3DGPN%26utm_campaign%3D205459%26mediaId%3D516",
                                           merchantName = (string)p["merchantName"]
                                       }).OrderByDescending(c => c.discount).FirstOrDefault();
                finaldata.Add(tablerowshealth);


                List<int> beauti_spa = new List<int>();

                var tablerowsbeauty = (from p in json["beauty"]

                                       select new
                                       {
                                           subcatagoryitem = "Beauty",
                                           description = "",
                                           discount = (int)p["discountPercent"],
                                           category = "0",
                                           promocode = "0",
                                           Url = (string)p["dealUrl"],
                                           merchantName = (string)p["merchantName"]
                                       }).OrderByDescending(c => c.discount).FirstOrDefault();
                beauti_spa.Add(tablerowsbeauty.discount);
                var tablerowsbeauty_spa = (from p in json["wellness"]

                                           select new
                                           {
                                               subcatagoryitem = "Beauty",
                                               description = "",
                                               discount = (int)p["discountPercent"],
                                               category = "0",
                                               promocode = "0",
                                               Url = (string)p["dealUrl"],
                                               merchantName = (string)p["merchantName"]
                                           }).OrderByDescending(c => c.discount).FirstOrDefault();
                beauti_spa.Add(tablerowsbeauty_spa.discount);

                var finaldiscountonbeauti = beauti_spa.Max();


                var finaldataforbeauti = (from p in beauti_spa

                                          select new
                                          {
                                              subcatagoryitem = "Beauty",
                                              description = "",
                                              discount = finaldiscountonbeauti,
                                              category = "0",
                                              promocode = "0",
                                              Url = "http://t.groupon.co.in/r?tsToken=IN_AFF_0_205459_516_0&url=http%3A%2F%2Fcity.groupon.co.in%2Fcoupons%2Fdelhi-ncr%2Fbeauty-and-spa%2F%3FCID%3DIN_AFF_5600_225_5383_1%26nlp%26utm_medium%3Dafl%26utm_source%3DGPN%26utm_campaign%3D205459%26mediaId%3D516",
                                              merchantName = ""
                                          }).OrderByDescending(c => c.discount).FirstOrDefault(); ;

                finaldata.Add(finaldataforbeauti);


                var resturant = (from p in json["restaurant"]

                                 select new
                                 {
                                     subcatagoryitem = "restaurant",
                                     description = "",
                                     discount = (int)p["discountPercent"],
                                     category = "0",
                                     promocode = "0",
                                     Url = "http://t.groupon.co.in/r?tsToken=IN_AFF_0_205459_516_0&url=http%3A%2F%2Fcity.groupon.co.in%2Fcoupons%2Fdelhi-ncr%2Ffood-and-drink%2F%3FCID%3DIN_AFF_5600_225_5383_1%26nlp%26utm_medium%3Dafl%26utm_source%3DGPN%26utm_campaign%3D205459%26mediaId%3D516",
                                     merchantName = (string)p["merchantName"]
                                 }).OrderByDescending(c => c.discount).FirstOrDefault();

                finaldata.Add(resturant);

                var accessories = (from p in json["products__accessories1"]

                                   select new
                                   {
                                       subcatagoryitem = "Accessories",
                                       description = "",
                                       discount = (int)p["discountPercent"],
                                       category = "0",
                                       promocode = "0",
                                       Url = "http://t.groupon.co.in/r?tsToken=IN_AFF_0_205459_516_0&url=http%3A%2F%2Fproducts.groupon.co.in%2Fshopping%2Faccessories%2F%3FCID%3DIN_AFF_5600_225_5383_1%26nlp%26utm_medium%3Dafl%26utm_source%3DGPN%26utm_campaign%3D205459%26mediaId%3D516",
                                       merchantName = (string)p["merchantName"]
                                   }).OrderByDescending(c => c.discount).FirstOrDefault();

                finaldata.Add(accessories);

                var electronic = (from p in json["electronic"]

                                  select new
                                  {
                                      subcatagoryitem = "electronic",
                                      description = "",
                                      discount = (int)p["discountPercent"],
                                      category = "0",
                                      promocode = "0",
                                      Url = "http://t.groupon.co.in/r?tsToken=IN_AFF_0_205459_516_0&url=http%3A%2F%2Fproducts.groupon.co.in%2Fshopping%2Felectronics%2F%3FCID%3DIN_AFF_5600_225_5383_1%26nlp%26utm_medium%3Dafl%26utm_source%3DGPN%26utm_campaign%3D205459%26mediaId%3D516",
                                      merchantName = (string)p["merchantName"]
                                  }).OrderByDescending(c => c.discount).FirstOrDefault();

                finaldata.Add(electronic);


                var products__sport = (from p in json["products__sport"]

                                       select new
                                       {
                                           subcatagoryitem = "sport",
                                           description = "",
                                           discount = (int)p["discountPercent"],
                                           category = "0",
                                           promocode = "0",
                                           Url = "http://t.groupon.co.in/r?tsToken=IN_AFF_0_205459_516_0&url=http%3A%2F%2Fproducts.groupon.co.in%2Fshopping%2Fsports-and-fitness%2F%3FCID%3DIN_AFF_5600_225_5383_1%26nlp%26utm_medium%3Dafl%26utm_source%3DGPN%26utm_campaign%3D205459%26mediaId%3D516",
                                           merchantName = (string)p["merchantName"]
                                       }).OrderByDescending(c => c.discount).FirstOrDefault();

                finaldata.Add(products__sport);
                var computer = (from p in json["computer"]

                                select new
                                {
                                    subcatagoryitem = "computer",
                                    description = "",
                                    discount = (int)p["discountPercent"],
                                    category = "0",
                                    promocode = "0",
                                    Url = "http://t.groupon.co.in/r?tsToken=IN_AFF_0_205459_516_0&url=http%3A%2F%2Fproducts.groupon.co.in%2Fshopping%2Felectronics%2Fcomputer-accessories%3FCID%3DIN_AFF_5600_225_5383_1%26nlp%26utm_medium%3Dafl%26utm_source%3DGPN%26utm_campaign%3D205459%26mediaId%3D516",
                                    merchantName = (string)p["merchantName"]
                                }).OrderByDescending(c => c.discount).FirstOrDefault();

                finaldata.Add(computer);
                var mobile_phone = (from p in json["mobile_phone"]

                                    select new
                                    {
                                        subcatagoryitem = "mobilephone",
                                        description = "",
                                        discount = (int)p["discountPercent"],
                                        category = "0",
                                        promocode = "0",
                                        Url = "http://t.groupon.co.in/r?tsToken=IN_AFF_0_205459_516_0&url=http%3A%2F%2Fproducts.groupon.co.in%2Fshopping%2Felectronics%2F%3FCID%3DIN_AFF_5600_225_5383_1%26nlp%26utm_medium%3Dafl%26utm_source%3DGPN%26utm_campaign%3D205459%26mediaId%3D516",
                                        merchantName = (string)p["merchantName"]
                                    }).OrderByDescending(c => c.discount).FirstOrDefault();

                finaldata.Add(mobile_phone);

                var toys = (from p in json["toy"]

                            select new
                            {
                                subcatagoryitem = "Toys_Games",
                                description = "",
                                discount = (int)p["discountPercent"],
                                category = (string)p["category"],
                                promocode = (string)p["promocode"],
                                Url = "http://t.groupon.co.in/r?tsToken=IN_AFF_0_205459_516_0&url=http%3A%2F%2Fproducts.groupon.co.in%2Fshopping%2Fbaby-kids-and-toys%2F%3FCID%3DIN_AFF_5600_225_5383_1%26nlp%26utm_medium%3Dafl%26utm_source%3DGPN%26utm_campaign%3D205459%26mediaId%3D516",
                                merchantName = (string)p["merchantName"]
                            }).OrderByDescending(c => c.discount).FirstOrDefault();

                finaldata.Add(toys);

                var jewellery = (from p in json["jewellery"]

                                 select new
                                 {
                                     subcatagoryitem = "Jewellery",
                                     description = "",
                                     discount = (int)p["discountPercent"],
                                     category = (string)p["category"],
                                     promocode = (string)p["promocode"],
                                     Url = "http://t.groupon.co.in/r?tsToken=IN_AFF_0_205459_516_0&url=http%3A%2F%2Fproducts.groupon.co.in%2Fshopping%2Fwomen%2Faccessories%3FCID%3DIN_AFF_5600_225_5383_1%26nlp%26utm_medium%3Dafl%26utm_source%3DGPN%26utm_campaign%3D205459%26mediaId%3D516",
                                     merchantName = (string)p["merchantName"]
                                 }).OrderByDescending(c => c.discount).FirstOrDefault();

                finaldata.Add(jewellery);












                List<int> menCollection = new List<int>();
                List<int> womenCollection = new List<int>();
                List<int> kidsCollection = new List<int>();

                var men_fashion = (from p in json["mens_fashion"]

                                   select new
                                   {
                                       subcatagoryitem = "mens_fashion",
                                       description = (string)p["title"],
                                       discount = (int)p["discountPercent"],
                                       category = (string)p["category"],
                                       promocode = (string)p["promocode"],
                                       Url = (string)p["dealUrl"],
                                       merchantName = (string)p["merchantName"]
                                   }).GroupBy(t => t.description).ToList();

                for (int j = 0; j < men_fashion.Count; j++)
                {
                    var datamen = men_fashion[j];
                    foreach (var a in datamen)
                    {
                        //var test = a.description.ToLower().IndexOf(" women ");
                        if (a.description.ToLower().IndexOf("for men") >= 0)
                        {
                            menCollection.Add(a.discount);
                        }
                        else { }

                    }

                }
                var womens_fashion = (from p in json["womens_fashion"]

                                      select new
                                      {
                                          subcatagoryitem = "womens_fashion",
                                          description = (string)p["title"],
                                          discount = (int)p["discountPercent"],
                                          category = (string)p["category"],
                                          promocode = (string)p["promocode"],
                                          Url = (string)p["dealUrl"],
                                          merchantName = (string)p["merchantName"]
                                      }).GroupBy(t => t.description).ToList();
                for (int j = 0; j < womens_fashion.Count; j++)
                {
                    var datawomen = womens_fashion[j];
                    foreach (var a in datawomen)
                    {
                        womenCollection.Add(a.discount);

                    }

                }
                //var men_fashion1 = new List<Ite>; 

                var kids_fashion = (from p in json["kids_fashion"]

                                    select new
                                    {
                                        subcatagoryitem = "Clothing Kids",
                                        description = (string)p["title"],
                                        discount = (int)p["discountPercent"],
                                        category = (string)p["category"],
                                        promocode = (string)p["promocode"],
                                        Url = (string)p["dealUrl"],
                                        merchantName = (string)p["merchantName"]
                                    }).GroupBy(t => t.description).ToList();



                var fashion = (from p in json["fashion"]

                               select new
                               {
                                   subcatagoryitem = "fashion",
                                   description = (string)p["title"],
                                   discount = (int)p["discountPercent"],
                                   category = (string)p["category"],
                                   promocode = (string)p["promocode"],
                                   Url = (string)p["dealUrl"],
                                   merchantName = (string)p["merchantName"]
                               }).GroupBy(t => t.description).ToList();


                for (int i = 0; i < fashion.Count; i++)
                {
                    var data = fashion[i];
                    foreach (var a in data)
                    {
                        if (a.description.ToLower().IndexOf("for men") >= 0)
                        {
                            menCollection.Add(a.discount);
                        }
                        else if (a.description.ToLower().IndexOf("for women") >= 0 || a.description.ToLower().IndexOf("sarees") >= 0)
                        {
                            womenCollection.Add(a.discount);

                        }

                        else if (a.description.ToLower().IndexOf(" kids") >= 0 || a.description.ToLower().IndexOf(" toys") >= 0)
                        {
                            kidsCollection.Add(a.discount);

                        }

                    }
                    //if (fashion[i].Where(c => c.description.IndexOf(" men ") >= 0)){}
                    //int indexofman = title.
                }

                // var men_fashion1 = men_fashion.GroupBy(c=>c.)


                var menfashiondiscount = menCollection.Max();
                var womenfashiondiscount = womenCollection.Max();
                var kidsfashiondiscount = kidsCollection.Max();



                var menfashiondata = (from p in menCollection

                                      select new
                                      {
                                          subcatagoryitem = "Clothing_Men",
                                          description = "",
                                          discount = menfashiondiscount,
                                          category = "0",
                                          promocode = "0",
                                          Url = "http://t.groupon.co.in/r?tsToken=IN_AFF_0_205459_516_0&url=http%3A%2F%2Fproducts.groupon.co.in%2Fshopping%2Fmen%2Fclothing%3FCID%3DIN_AFF_5600_225_5383_1%26nlp%26utm_medium%3Dafl%26utm_source%3DGPN%26utm_campaign%3D205459%26mediaId%3D516",
                                          merchantName = ""
                                      }).OrderByDescending(c => c.discount).FirstOrDefault();


                var womenfashiondata = (from p in womenCollection

                                        select new
                                        {
                                            subcatagoryitem = "Clothing_Women",
                                            description = "",
                                            discount = womenfashiondiscount,
                                            category = "0",
                                            promocode = "0",
                                            Url = "http://t.groupon.co.in/r?tsToken=IN_AFF_0_205459_516_0&url=http%3A%2F%2Fproducts.groupon.co.in%2Fshopping%2Fwomen%2Fethnic-wear%3FCID%3DIN_AFF_5600_225_5383_1%26nlp%26utm_medium%3Dafl%26utm_source%3DGPN%26utm_campaign%3D205459%26mediaId%3D516",
                                            merchantName = ""
                                        }).OrderByDescending(c => c.discount).FirstOrDefault();


                var kidsfashiondata = (from p in kidsCollection

                                       select new
                                       {
                                           subcatagoryitem = "Clothing_kids",
                                           description = "",
                                           discount = kidsfashiondiscount,
                                           category = "0",
                                           promocode = "0",
                                           Url = "http://t.groupon.co.in/r?tsToken=IN_AFF_0_205459_516_0&url=http%3A%2F%2Fproducts.groupon.co.in%2Fshopping%2Fbaby-kids-and-toys%2Fkids-clothing-and-footwear%2F%3FCID%3DIN_AFF_5600_225_5383_1%26nlp%26utm_medium%3Dafl%26utm_source%3DGPN%26utm_campaign%3D205459%26mediaId%3D516",
                                           merchantName = ""
                                       }).OrderByDescending(c => c.discount).FirstOrDefault();



                finaldata.Add(menfashiondata);
                finaldata.Add(womenfashiondata);
                finaldata.Add(kidsfashiondata);


                var specialDeal = (from p in json["specialDeal"]

                                   select new
                                   {
                                       subcatagoryitem = "ccd",
                                       description = (string)p["title"],
                                       discount = 0,
                                       category = "0",
                                       promocode = "0",
                                       Url = "http://t.groupon.co.in/r?tsToken=IN_AFF_0_205459_516_0&url=http%3A%2F%2Fcity.groupon.co.in%2Fdeals%2Fdelhi-ncr%2Fcafe-coffee-day-5%2F851767%3Fid=55bbbee1f3b9ca1b59a9ff05%3FCID%3DIN_AFF_5600_225_5383_1%26nlp%26utm_medium%3Dafl%26utm_source%3DGPN%26utm_campaign%3D205459%26mediaId%3D516",
                                       merchantName = ""
                                   }).OrderByDescending(c => c.discount).FirstOrDefault();



                finaldata.Add(specialDeal);




                var promocode = (from p in json["promocode"]

                                 select new
                                 {
                                     subcatagoryitem = "promocode",
                                     description = (string)p["title"],
                                     shortannoucementtitle = (string)p["shortAnnouncementTitle"],
                                     discount = 0,
                                     category = (string)p["category"],
                                     promocode = "0",
                                     Url = "",
                                     merchantName = ""
                                 }).GroupBy(t => t.description).ToList();

                for (int i = 0; i < promocode.Count; i++)
                {
                    var data = promocode[i];
                    foreach (var a in data)
                    {
                        if (a.description.ToLower().IndexOf("use coupon") >= 0 || a.description.ToLower().IndexOf("use code") >= 0 || a.description.ToLower().IndexOf("use promo") >= 0)
                        {
                            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
                            string[] words = a.description.Split(delimiterChars);
                            int index = Array.IndexOf(words, words.Where(x => x.ToLower().Contains("code")).FirstOrDefault());
                            int couponindex = 0;
                            if (words[index + 1] == "-" || words[index + 1] == ":" || words[index + 1] == "")
                            {
                                couponindex = index + 2;
                            }
                            else
                            {
                                couponindex = index + 1;
                            }


                            var couponcode = words[couponindex];


                            var promocode1 = (from p in json["promocode"]

                                              select new
                                              {
                                                  subcatagoryitem = "promocode",
                                                  description = (string)p["title"],
                                                  discount = 0,//(int)p["discountPercent"],
                                                  category = (string)p["category"],
                                                  promocode = couponcode,
                                                  Url = "",
                                                  merchantName = ""
                                              }).OrderByDescending(c => c.discount).FirstOrDefault();


                            finaldata.Add(promocode1);
                        }
                        else if (a.shortannoucementtitle.ToLower().IndexOf("use coupon") >= 0 || a.shortannoucementtitle.ToLower().IndexOf("use code") >= 0 || a.shortannoucementtitle.ToLower().IndexOf("use promo") >= 0)
                        {
                            char[] delimiterChars = { ' ', ',', '.', ':', '\t', '-' };
                            string[] words = a.shortannoucementtitle.Split(delimiterChars);
                            int index = Array.IndexOf(words, words.Where(x => x.ToLower().Contains("code")).FirstOrDefault());
                            int couponindex = 0;
                            if (words[index + 1] == "-" || words[index + 1] == ":" || words[index + 1] == "")
                            {
                                couponindex = index + 2;
                            }
                            else
                            {
                                couponindex = index + 1;
                            }


                            var couponcode = words[couponindex];


                            var promocode1 = (from p in json["promocode"]

                                              select new
                                              {
                                                  subcatagoryitem = "promocode",
                                                  description = (string)p["title"],
                                                  discount = 0, //(int)p["discountPercent"],
                                                  category = (string)p["category"],
                                                  promocode = couponcode,
                                                  Url = "",
                                                  merchantName = ""
                                              }).OrderByDescending(c => c.discount).FirstOrDefault();


                            finaldata.Add(promocode1);

                        }

                        else
                        {
                            //kidsCollection.Add(a.discount);

                        }

                    }

                }


                //var men_fashion2 = men_fashion1. 

                for (int i = 0; i < finaldata.Count; i++)
                {
                    Object a = finaldata[i];
                    if (a != null)
                    {

                        var subcategory = a.GetType().GetProperty("subcatagoryitem").GetValue(a, null);
                        var description = a.GetType().GetProperty("description").GetValue(a, null);
                        var discount = a.GetType().GetProperty("discount").GetValue(a, null);
                        var url = a.GetType().GetProperty("Url").GetValue(a, null);
                        CuponDetail objCuponDetail = new CuponDetail();
                        if (subcategory == "Hotel")
                        {
                            objCuponDetail.CategoriesId = 3;
                            objCuponDetail.WebsitesId = 12;
                            objCuponDetail.Title = "Upto " + discount + "% Off On All Hotel Deals";


                            objCuponDetail.ImageurlId = 91;
                            objCuponDetail.Description = "Get Upto " + discount + "% Off On All Hotel Deals Across The Store. No Minimum Booking & No Coupon Code Required To Avail The Discount."
    ;
                            objCuponDetail.CouponType = 1;
                            objCuponDetail.RedirectType = 2;
                            objCuponDetail.RedirectUrl = url.ToString();
                            objCuponDetail.CuponCode = "0";
                            objCuponDetail.IsActive = Convert.ToInt16(Isactive.Active);
                            objCuponDetail.CreatedOn = DateTime.Now.AddDays(-20);
                            objCuponDetail.ModifiedOn = DateTime.Now.AddDays(-20);
                            objCuponDetail.SubcategoriesID = 9;
                            listcupondetails_groupon.Add(objCuponDetail);
                        }

                        else if (subcategory == "Tour")
                        {
                            objCuponDetail.CategoriesId = 3;
                            objCuponDetail.WebsitesId = 12;
                            objCuponDetail.Title = "Upto " + discount + "% Off On All Tour Package Deals";
                            objCuponDetail.ImageurlId = 91;
                            objCuponDetail.Description = "Get Upto " + discount + "% Off On All Tour Package related Deals Across The Store. No Minimum Booking & No Coupon Code Required To Avail The Discount.";
                            objCuponDetail.CouponType = 1;
                            objCuponDetail.RedirectType = 2;
                            objCuponDetail.RedirectUrl = url.ToString();
                            objCuponDetail.CuponCode = "0";
                            objCuponDetail.IsActive = Convert.ToInt16(Isactive.Active);
                            objCuponDetail.CreatedOn = DateTime.Now.AddDays(-20);
                            objCuponDetail.ModifiedOn = DateTime.Now.AddDays(-20);
                            objCuponDetail.SubcategoriesID = 32;
                            listcupondetails_groupon.Add(objCuponDetail);
                        }

                        else if (subcategory == "leisure")
                        {
                            objCuponDetail.CategoriesId = 3;
                            objCuponDetail.WebsitesId = 12;
                            objCuponDetail.Title = "Upto " + discount + "% Off On All Gateways & Day Outing Deals";
                            objCuponDetail.ImageurlId = 91;
                            objCuponDetail.Description = "Get Upto " + discount + "% Off On All Gateways & Day Outing Deals Across The Store. No Minimum Booking & No Coupon Code Required To Avail The Discount.";
                            objCuponDetail.CouponType = 1;
                            objCuponDetail.RedirectType = 2;
                            objCuponDetail.RedirectUrl = url.ToString();
                            objCuponDetail.CuponCode = "0";
                            objCuponDetail.IsActive = Convert.ToInt16(Isactive.Active);
                            objCuponDetail.CreatedOn = DateTime.Now.AddDays(-20);
                            objCuponDetail.ModifiedOn = DateTime.Now.AddDays(-20);
                            objCuponDetail.SubcategoriesID = 35;
                            listcupondetails_groupon.Add(objCuponDetail);
                        }




                        else if (subcategory == "electronic")
                        {
                            objCuponDetail.CategoriesId = 2;
                            objCuponDetail.WebsitesId = 12;
                            objCuponDetail.Title = "Upto " + discount + "% off on all electronics goods ";
                            objCuponDetail.ImageurlId = 91;
                            objCuponDetail.Description = "Get Upto " + discount + "% Off On All electronics goods related Deals Across The Store. No Minimum Booking & No Coupon Code Required To Avail The Discount.";
                            objCuponDetail.CouponType = 1;
                            objCuponDetail.RedirectType = 2;
                            objCuponDetail.RedirectUrl = url.ToString();
                            objCuponDetail.CuponCode = "";
                            objCuponDetail.IsActive = Convert.ToInt16(Isactive.Active);
                            objCuponDetail.CreatedOn = DateTime.Now.AddDays(-20);
                            objCuponDetail.ModifiedOn = DateTime.Now.AddDays(-20);
                            objCuponDetail.SubcategoriesID = 34;
                            listcupondetails_groupon.Add(objCuponDetail);
                        }

                        else if (subcategory == "computer")
                        {
                            objCuponDetail.CategoriesId = 2;
                            objCuponDetail.WebsitesId = 12;
                            objCuponDetail.Title = "Buy Computer & Laptop Accessories at low prices in India on Groupon.";
                            objCuponDetail.ImageurlId = 91;
                            objCuponDetail.Description = "Get Upto " + discount + "% Off On All Computer & Laptop Accessories. No Minimum Booking & No Coupon Code Required To Avail The Discount.";
                            objCuponDetail.CouponType = 1;
                            objCuponDetail.RedirectType = 2;
                            objCuponDetail.RedirectUrl = url.ToString();
                            objCuponDetail.CuponCode = "";
                            objCuponDetail.IsActive = Convert.ToInt16(Isactive.Active);
                            objCuponDetail.CreatedOn = DateTime.Now.AddDays(-20);
                            objCuponDetail.ModifiedOn = DateTime.Now.AddDays(-20);
                            objCuponDetail.SubcategoriesID = 2;
                            listcupondetails_groupon.Add(objCuponDetail);
                        }
                        else if (subcategory == "mobilephone")
                        {
                            objCuponDetail.CategoriesId = 2;
                            objCuponDetail.WebsitesId = 12;
                            objCuponDetail.Title = "Find the hottest deals on mobiles,tablets & accessories";
                            objCuponDetail.ImageurlId = 91;
                            objCuponDetail.Description = "Get Upto " + discount + "% Off On All mobiles,tablets & accessories. No Minimum Booking & No Coupon Code Required To Avail The Discount.";
                            objCuponDetail.CouponType = 1;
                            objCuponDetail.RedirectType = 2;
                            objCuponDetail.RedirectUrl = url.ToString();
                            objCuponDetail.CuponCode = "";
                            objCuponDetail.IsActive = Convert.ToInt16(Isactive.Active);
                            objCuponDetail.CreatedOn = DateTime.Now.AddDays(-20);
                            objCuponDetail.ModifiedOn = DateTime.Now.AddDays(-20);
                            objCuponDetail.SubcategoriesID = 34;
                            listcupondetails_groupon.Add(objCuponDetail);
                        }



                        else if (subcategory == "ccd")
                        {
                            objCuponDetail.CategoriesId = 4;
                            objCuponDetail.WebsitesId = 12;
                            objCuponDetail.Title = "Cafe Cofee Day ";
                            objCuponDetail.ImageurlId = 91;
                            objCuponDetail.Description = description.ToString();
                            objCuponDetail.CouponType = 1;
                            objCuponDetail.RedirectType = 2;
                            objCuponDetail.RedirectUrl = url.ToString();
                            objCuponDetail.CuponCode = "";
                            objCuponDetail.IsActive = Convert.ToInt16(Isactive.Active);
                            objCuponDetail.CreatedOn = DateTime.Now.AddDays(-20);
                            objCuponDetail.ModifiedOn = DateTime.Now.AddDays(-20);
                            objCuponDetail.SubcategoriesID = 29;
                            listcupondetails_groupon.Add(objCuponDetail);
                        }


                        else if (subcategory == "Health")
                        {
                            objCuponDetail.CategoriesId = 6;
                            objCuponDetail.WebsitesId = 12;
                            objCuponDetail.Title = "Upto " + discount + "% off on Healthcare ";
                            objCuponDetail.ImageurlId = 91;
                            objCuponDetail.Description = "Get Upto " + discount + "% Off On All Healthcare related Deals Across The Store. No Minimum Booking & No Coupon Code Required To Avail The Discount.";
                            objCuponDetail.CouponType = 1;
                            objCuponDetail.RedirectType = 2;
                            objCuponDetail.RedirectUrl = url.ToString();
                            objCuponDetail.CuponCode = "";
                            objCuponDetail.IsActive = Convert.ToInt16(Isactive.Active);
                            objCuponDetail.CreatedOn = DateTime.Now.AddDays(-20);
                            objCuponDetail.ModifiedOn = DateTime.Now.AddDays(-20);
                            objCuponDetail.SubcategoriesID = 14;
                            listcupondetails_groupon.Add(objCuponDetail);
                        }

                        else if (subcategory == "sport")
                        {
                            objCuponDetail.CategoriesId = 6;
                            objCuponDetail.WebsitesId = 12;
                            objCuponDetail.Title = "Upto " + discount + "% off on Sports & Fitness ";
                            objCuponDetail.ImageurlId = 91;
                            objCuponDetail.Description = "Get Upto " + discount + "% Off On All Sports Equipment & Fitness Accessories. No Minimum Booking & No Coupon Code Required To Avail The Discount.";
                            objCuponDetail.CouponType = 1;
                            objCuponDetail.RedirectType = 2;
                            objCuponDetail.RedirectUrl = url.ToString();
                            objCuponDetail.CuponCode = "";
                            objCuponDetail.IsActive = Convert.ToInt16(Isactive.Active);
                            objCuponDetail.CreatedOn = DateTime.Now.AddDays(-20);
                            objCuponDetail.ModifiedOn = DateTime.Now.AddDays(-20);
                            objCuponDetail.SubcategoriesID = 18;
                            listcupondetails_groupon.Add(objCuponDetail);
                        }

                        else if (subcategory == "Toys_Games")
                        {
                            objCuponDetail.CategoriesId = 6;
                            objCuponDetail.WebsitesId = 12;
                            objCuponDetail.Title = "Toys & Games";
                            objCuponDetail.ImageurlId = 91;
                            objCuponDetail.Description = "Buy Toys & Games online and Get Upto " + discount + "% Off. No Minimum Booking & No Coupon Code Required To Avail The Discount.";
                            objCuponDetail.CouponType = 1;
                            objCuponDetail.RedirectType = 2;
                            objCuponDetail.RedirectUrl = url.ToString();
                            objCuponDetail.CuponCode = "";
                            objCuponDetail.IsActive = Convert.ToInt16(Isactive.Active);
                            objCuponDetail.CreatedOn = DateTime.Now.AddDays(-20);
                            objCuponDetail.ModifiedOn = DateTime.Now.AddDays(-20);
                            objCuponDetail.SubcategoriesID = 31;
                            listcupondetails_groupon.Add(objCuponDetail);
                        }



                        else if (subcategory == "Accessories")
                        {
                            objCuponDetail.CategoriesId = 1;
                            objCuponDetail.WebsitesId = 12;
                            objCuponDetail.Title = "Upto " + discount + "% Off On Wide Range Of product accessories ";
                            objCuponDetail.ImageurlId = 91;
                            objCuponDetail.Description = "Get upto " + discount + "% Off On all product accessories deals across the store. No Minimum Booking & No Coupon Code Required To Avail The Discount.";
                            objCuponDetail.CouponType = 1;
                            objCuponDetail.RedirectType = 2;
                            objCuponDetail.RedirectUrl = url.ToString();
                            objCuponDetail.CuponCode = "";
                            objCuponDetail.IsActive = Convert.ToInt16(Isactive.Active);
                            objCuponDetail.CreatedOn = DateTime.Now.AddDays(-20);
                            objCuponDetail.ModifiedOn = DateTime.Now.AddDays(-20);
                            objCuponDetail.SubcategoriesID = 33;
                            listcupondetails_groupon.Add(objCuponDetail);
                        }
                        else if (subcategory == "Clothing_Women")
                        {
                            objCuponDetail.CategoriesId = 1;
                            objCuponDetail.WebsitesId = 12;
                            objCuponDetail.Title = "Online Shopping Sale – Upto " + discount + "% Discount on Womens clothing";
                            objCuponDetail.ImageurlId = 91;
                            objCuponDetail.Description = "Get Discount upto " + discount + "% at Groupon Online Shopping sale for women clothing. No Minimum Booking & No Coupon Code Required To Avail The Discount.";
                            objCuponDetail.CouponType = 1;
                            objCuponDetail.RedirectType = 2;
                            objCuponDetail.RedirectUrl = url.ToString();
                            objCuponDetail.CuponCode = "";
                            objCuponDetail.IsActive = Convert.ToInt16(Isactive.Active);
                            objCuponDetail.CreatedOn = DateTime.Now.AddDays(-20);
                            objCuponDetail.ModifiedOn = DateTime.Now.AddDays(-20);
                            objCuponDetail.SubcategoriesID = 12;
                            listcupondetails_groupon.Add(objCuponDetail);
                        }

                        else if (subcategory == "Jewellery")
                        {
                            objCuponDetail.CategoriesId = 1;
                            objCuponDetail.WebsitesId = 12;
                            objCuponDetail.Title = "Upto " + discount + "% Off On Jewellery.";
                            objCuponDetail.ImageurlId = 91;
                            objCuponDetail.Description = "Get upto " + discount + "% Off On all Jewellery. No Minimum Booking & No Coupon Code Required To Avail The Discount.";
                            objCuponDetail.CouponType = 1;
                            objCuponDetail.RedirectType = 2;
                            objCuponDetail.RedirectUrl = url.ToString();
                            objCuponDetail.CuponCode = "";
                            objCuponDetail.IsActive = Convert.ToInt16(Isactive.Active);
                            objCuponDetail.CreatedOn = DateTime.Now.AddDays(-20);
                            objCuponDetail.ModifiedOn = DateTime.Now.AddDays(-20);
                            objCuponDetail.SubcategoriesID = 23;
                            listcupondetails_groupon.Add(objCuponDetail);
                        }

                        else if (subcategory == "Accessories")
                        {
                            objCuponDetail.CategoriesId = 1;
                            objCuponDetail.WebsitesId = 12;
                            objCuponDetail.Title = "Online Shopping Sale – Upto " + discount + "% Discount on Mens clothing";
                            objCuponDetail.ImageurlId = 91;
                            objCuponDetail.Description = "Get Discount upto " + discount + "% at Groupon Online Shopping sale for women clothing. No Minimum Booking & No Coupon Code Required To Avail The Discount.";
                            objCuponDetail.CouponType = 1;
                            objCuponDetail.RedirectType = 2;
                            objCuponDetail.RedirectUrl = url.ToString();
                            objCuponDetail.CuponCode = "";
                            objCuponDetail.IsActive = Convert.ToInt16(Isactive.Active);
                            objCuponDetail.CreatedOn = DateTime.Now.AddDays(-20);
                            objCuponDetail.ModifiedOn = DateTime.Now.AddDays(-20);
                            objCuponDetail.SubcategoriesID =33;
                            listcupondetails_groupon.Add(objCuponDetail);
                        }





                        else if (subcategory == "Beauty")
                        {
                            objCuponDetail.CategoriesId = 6;
                            objCuponDetail.WebsitesId = 12;
                            objCuponDetail.Title = "Upto " + discount + "% Off On Wide Range Of Beauty & Spa Deals";
                            objCuponDetail.ImageurlId = 91;
                            objCuponDetail.Description = "Get Upto " + discount + "% Off On Beauty & Spa. No Minimum Purchase Required. No coupon Code Required To Avail The Offer.";
                            objCuponDetail.CouponType = 1;
                            objCuponDetail.RedirectType = 2;
                            objCuponDetail.RedirectUrl = url.ToString();
                            objCuponDetail.CuponCode = "0";
                            objCuponDetail.IsActive = Convert.ToInt16(Isactive.Active);
                            objCuponDetail.CreatedOn = DateTime.Now.AddDays(-20);
                            objCuponDetail.ModifiedOn = DateTime.Now.AddDays(-20);
                            objCuponDetail.SubcategoriesID = 13;
                            listcupondetails_groupon.Add(objCuponDetail);
                        }


                        else if (subcategory == "restaurant")
                        {
                            objCuponDetail.CategoriesId = 4;
                            objCuponDetail.WebsitesId = 12;
                            objCuponDetail.Title = "Amezing offer! Save Upto " + discount + "% with great resturant Deals in your city";
                            objCuponDetail.ImageurlId = 91;
                            objCuponDetail.Description = "Restaurant Deals - Save! discounts upto " + discount + "% at fine dining restaurants in your city.No Minimum Purchase Required. No coupon Code Required To Avail The Offer.";
                            objCuponDetail.CouponType = 1;
                            objCuponDetail.RedirectType = 2;
                            objCuponDetail.RedirectUrl = url.ToString();
                            objCuponDetail.CuponCode = "0";
                            objCuponDetail.IsActive = Convert.ToInt16(Isactive.Active);
                            objCuponDetail.CreatedOn = DateTime.Now.AddDays(-20);
                            objCuponDetail.ModifiedOn = DateTime.Now.AddDays(-20);
                            objCuponDetail.SubcategoriesID = 4;
                            listcupondetails_groupon.Add(objCuponDetail);
                        }




                        //var promocode = (from p in json["promocode"]

                        //                 select new
                        //                 {
                        //                     subcatagoryitem = "promocode",
                        //                     description = (string)p["title"],
                        //                     discount = (int)p["discountPercent"],
                        //                     category = (string)p["category"],
                        //                     promocode = (string)p["promocode"],
                        //                     Url = (string)p["dealUrl"],
                        //                     merchantName = (string)p["merchantName"]
                        //                 }).GroupBy(t => t.description).Select(g => g.First()).ToList();
                        //var finalarray_groupon = promocode.Concat(tableRowshotel).Concat(tableRowstour).Concat(tablerowshealth).Concat(tablerowsbeauty).Concat(tablerowsbeauty_spa).Concat(resturant);


                        //foreach (var a in finalarray_groupon)
                        //{
                        //    CuponDetail objCuponDetail = new CuponDetail();
                        //    if (a.subcatagoryitem == "Hotel")
                        //    {
                        //        objCuponDetail.CategoriesId = 3;
                        //        objCuponDetail.WebsitesId = 12;
                        //        if (a.discount == 0)
                        //        {
                        //            objCuponDetail.Title = a.merchantName;
                        //        }
                        //        else
                        //        {
                        //            objCuponDetail.Title = "Get " + a.discount + "% discount at " + a.merchantName;
                        //        }

                        //        objCuponDetail.ImageurlId = 91;
                        //        objCuponDetail.Description = a.description;
                        //        objCuponDetail.CouponType = 1;
                        //        objCuponDetail.RedirectType = 2;
                        //        objCuponDetail.RedirectUrl = a.Url;
                        //        objCuponDetail.CuponCode = a.promocode;
                        //        objCuponDetail.IsActive = Convert.ToInt16(Isactive.Active);
                        //        objCuponDetail.CreatedOn = DateTime.Now;
                        //        objCuponDetail.ModifiedOn = DateTime.Now;
                        //        objCuponDetail.SubcategoriesID = 9;
                        //        listcupondetails_groupon.Add(objCuponDetail);
                        //    }

                        //    else if (a.subcatagoryitem == "promocode")
                        //    {

                        //        objCuponDetail.WebsitesId = 12;

                        //        objCuponDetail.Title = a.description;
                        //        objCuponDetail.ImageurlId = 91;
                        //        objCuponDetail.Description = "Use coupon code and get amazing discount";
                        //        objCuponDetail.CouponType = 1;
                        //        objCuponDetail.RedirectType = 1;
                        //        objCuponDetail.RedirectUrl = a.Url;
                        //        objCuponDetail.CuponCode = a.promocode;
                        //        objCuponDetail.IsActive = Convert.ToInt16(Isactive.Active);
                        //        objCuponDetail.CreatedOn = DateTime.Now;
                        //        objCuponDetail.ModifiedOn = DateTime.Now;

                        //        if (a.category == "accommodation")
                        //        {
                        //            objCuponDetail.CategoriesId = 3;
                        //            objCuponDetail.SubcategoriesID = 9;
                        //        }
                        //        else if (a.category == "healthcare")
                        //        {
                        //            objCuponDetail.CategoriesId = 6;
                        //            objCuponDetail.SubcategoriesID = 14;
                        //        }
                        //        else if (a.category == "beauty")
                        //        {
                        //            objCuponDetail.CategoriesId = 6;
                        //            objCuponDetail.SubcategoriesID = 13;
                        //        }
                        //        else if (a.category == "wellness")
                        //        {
                        //            objCuponDetail.CategoriesId = 6;
                        //            objCuponDetail.SubcategoriesID = 13;
                        //        }

                        //        else if (a.category == "restaurant")
                        //        {
                        //            objCuponDetail.CategoriesId = 4;
                        //            objCuponDetail.SubcategoriesID = 4;
                        //        }


                        //        else
                        //        {
                        //            objCuponDetail.CategoriesId = 3;
                        //            objCuponDetail.SubcategoriesID = 32;
                        //        }

                        //        //objCuponDetail.SubcategoriesID = 4;
                        //        listcupondetails_groupon.Add(objCuponDetail);
                        //    }
                        //    else if (a.subcatagoryitem == "Health")
                        //    {
                        //        objCuponDetail.CategoriesId = 6;
                        //        objCuponDetail.WebsitesId = 12;
                        //        if (a.discount == 0)
                        //        {
                        //            objCuponDetail.Title = a.merchantName;
                        //        }
                        //        else
                        //        {
                        //            objCuponDetail.Title = "Get " + a.discount + "% discount at " + a.merchantName;
                        //        }
                        //        objCuponDetail.ImageurlId = 91;
                        //        objCuponDetail.Description = a.description;
                        //        objCuponDetail.CouponType = 1;
                        //        objCuponDetail.RedirectType = 2;
                        //        objCuponDetail.RedirectUrl = a.Url;
                        //        objCuponDetail.CuponCode = a.promocode;
                        //        objCuponDetail.IsActive = Convert.ToInt16(Isactive.Active);
                        //        objCuponDetail.CreatedOn = DateTime.Now;
                        //        objCuponDetail.ModifiedOn = DateTime.Now;
                        //        objCuponDetail.SubcategoriesID = 14;
                        //        listcupondetails_groupon.Add(objCuponDetail);
                        //    }
                        //    else if (a.subcatagoryitem == "Beauty")
                        //    {
                        //        objCuponDetail.CategoriesId = 6;
                        //        objCuponDetail.WebsitesId = 12;
                        //        if (a.discount == 0)
                        //        {
                        //            objCuponDetail.Title = a.merchantName;
                        //        }
                        //        else
                        //        {
                        //            objCuponDetail.Title = "Get " + a.discount + "% discount at " + a.merchantName;
                        //        }
                        //        objCuponDetail.ImageurlId = 91;
                        //        objCuponDetail.Description = a.description;
                        //        objCuponDetail.CouponType = 1;
                        //        objCuponDetail.RedirectType = 2;
                        //        objCuponDetail.RedirectUrl = a.Url;
                        //        objCuponDetail.CuponCode = a.promocode;
                        //        objCuponDetail.IsActive = Convert.ToInt16(Isactive.Active);
                        //        objCuponDetail.CreatedOn = DateTime.Now;
                        //        objCuponDetail.ModifiedOn = DateTime.Now;
                        //        objCuponDetail.SubcategoriesID = 13;
                        //        listcupondetails_groupon.Add(objCuponDetail);
                        //    }
                        //    else if (a.subcatagoryitem == "restaurant")
                        //    {
                        //        objCuponDetail.CategoriesId = 4;
                        //        objCuponDetail.WebsitesId = 12;
                        //        if (a.discount == 0)
                        //        {
                        //            objCuponDetail.Title = a.merchantName;
                        //        }
                        //        else
                        //        {
                        //            objCuponDetail.Title = "Get " + a.discount + "% discount at " + a.merchantName;
                        //        }
                        //        objCuponDetail.ImageurlId = 91;
                        //        objCuponDetail.Description = a.description;
                        //        objCuponDetail.CouponType = 1;
                        //        objCuponDetail.RedirectType = 2;
                        //        objCuponDetail.RedirectUrl = a.Url;
                        //        objCuponDetail.CuponCode = a.promocode;
                        //        objCuponDetail.IsActive = Convert.ToInt16(Isactive.Active);
                        //        objCuponDetail.CreatedOn = DateTime.Now;
                        //        objCuponDetail.ModifiedOn = DateTime.Now;
                        //        objCuponDetail.SubcategoriesID = 4;
                        //        listcupondetails_groupon.Add(objCuponDetail);
                        //    }


                        //    else
                        //    {
                        //        objCuponDetail.CategoriesId = 3;
                        //        objCuponDetail.WebsitesId = 12;
                        //        if (a.discount == 0)
                        //        {
                        //            objCuponDetail.Title = a.merchantName;
                        //        }
                        //        else
                        //        {
                        //            objCuponDetail.Title = "Get " + a.discount + "% discount at " + a.merchantName;
                        //        }
                        //        objCuponDetail.ImageurlId = 91;
                        //        objCuponDetail.Description = a.description;
                        //        objCuponDetail.CouponType = 1;
                        //        objCuponDetail.RedirectType = 2;
                        //        objCuponDetail.RedirectUrl = a.Url;
                        //        objCuponDetail.CuponCode = a.promocode;
                        //        objCuponDetail.IsActive = Convert.ToInt16(Isactive.Active);
                        //        objCuponDetail.CreatedOn = DateTime.Now;
                        //        objCuponDetail.ModifiedOn = DateTime.Now;
                        //        objCuponDetail.SubcategoriesID = 32;
                        //        listcupondetails_groupon.Add(objCuponDetail);
                        //    }
                        //}



                        ////List<ApiUploadStatus> apiuploadlist = new List<ApiUploadStatus>();
                        //Api_Import_Status apistatus = new Api_Import_Status();
                        //ApiUploadStatus apiupload = new ApiUploadStatus();
                        //if (statusaction == true)
                        //{

                        //    apistatus.Date = DateTime.Now;
                        //    apistatus.api_Name = "Groupon";
                        //    apistatus.api_import_status1 = "completed";
                        //    apistatus.no_of_row = finalarray_groupon.Count();

                        //   // apiuploadstatus = apiupload.Save(apistatus);
                        ////}


                    }
                }
                statusaction = couponmodel.bulkSave(listcupondetails_groupon);
                Api_Import_Status apistatus = new Api_Import_Status();
                ApiUploadStatus apiupload = new ApiUploadStatus();
                if (statusaction == true)
                {

                    apistatus.Date = DateTime.Now;
                    apistatus.api_Name = "Groupon";
                    apistatus.api_import_status1 = "completed";
                    apistatus.no_of_row = listcupondetails_groupon.Count();

                   apiuploadstatus = apiupload.Save(apistatus);
                }
                return statusaction;
            }
            catch (Exception ex)
            {
                statusaction = false;
                ErrorLogger err = new ErrorLogger();
                Audit audit = new Audit();
                audit.Details = ex.InnerException + ex.StackTrace;
                audit.CreatedOn = DateTime.Now;
                audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.Customer);
                err.Save(audit);
                return statusaction;
            }
        }


    }
}