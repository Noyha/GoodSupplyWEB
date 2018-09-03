using GoodSupplyWEB.Models.DB;
using GoodSupplyWEB.Models;
using GoodSupplyWEB.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Collections;

namespace GoodSupplyWEB.Controllers
{
    public class BinPackingController : Controller
    {
        //משתנים קבועים למשאית
        int truckWidth = 120;
        int truckLength = 150;
        int truckHeigth = 90;

        public int truckL { get; set; }
        public int numBinW { get; set; }

        List<double> numbersW;
        List<double> numbersL;
        List<double> numbersH;


        public static List<Package> printPack;
        public static List<Package> packages;
        public static bool binFrame = false;
        public static bool binW = false;

        public List<Package> Packer(List<Package> packages, int truckWidth, int truckLength, int truckHeigth)
        {
            numbersW = new List<double>(packages.Count);
            numbersL = new List<double>(packages.Count);
            numbersH = new List<double>(packages.Count);

            // 3	נכניס ב-3 מערכים שונים (arrWidth, arrLength,arrHeigth ) את המימדים מכל החבילות בלולאת for 
            for (int i = 0; i < packages.Count; i++)
            {
                numbersW.Add(packages[i].packWidth);
                numbersL.Add(packages[i].packLength);
                numbersH.Add(packages[i].packHeigth);
            }

            numbersW.Sort();
            numbersL.Sort();
            numbersH.Sort();

            double tempWidth;
            double tempLength;
            double tempHeigth;

            if (packages.Count > 1)
            {
                tempWidth = numbersW.Last() / numbersW.First();
                tempLength = numbersL.Last() / numbersL.First();
                tempHeigth = numbersH.Last() / numbersH.First();
            }
            else
            {
                tempWidth = numbersW.Last();
                tempLength = numbersL.Last();
                tempHeigth = numbersH.Last();
            }

            int binWidth = 0;
            int binLength = 0;
            int binHeigth = 0;


            //למציאת ערך קטן,גדול,בינוני ועדכון אורך חדש
            if (tempWidth < tempLength && tempWidth < tempHeigth)
            {
                binLength = (int)numbersW.Last();
                changeWidthLength(packages);      //change column width with column length

                if (tempLength < tempHeigth)
                {
                    binWidth = (int)numbersH.Last();
                    binHeigth = (int)numbersL.Last();
                    changeWidhtHeigth(packages);
                }
                else if (tempLength > tempHeigth)
                {
                    binWidth = (int)numbersL.Last();
                    binHeigth = (int)numbersH.Last();
                }
                else
                {
                    binWidth = (int)numbersW.Last();
                    binHeigth = (int)numbersH.Last();
                }
            }
            else if (tempLength < tempWidth && tempLength < tempHeigth)
            {
                binLength = (int)numbersL.Last();

                if (tempWidth < tempHeigth)
                {
                    binWidth = (int)numbersH.Last();
                    binHeigth = (int)numbersW.Last();
                    changeWidhtHeigth(packages);    //change column width with column heigth
                }
                else if (tempWidth > tempHeigth)
                {
                    binWidth = (int)numbersW.Last();
                    binHeigth = (int)numbersH.Last();
                }
                else
                {
                    binWidth = (int)numbersW.Last();
                    binHeigth = (int)numbersH.Last();
                }
            }
            else if (tempHeigth < tempWidth && tempHeigth < tempLength)
            {
                binLength = (int)numbersH.Last();
                changeLengthHeigth(packages);      //change column heigth with column length

                if (tempWidth < tempLength)
                {
                    binWidth = (int)numbersL.Last();
                    binHeigth = (int)numbersW.Last();
                    changeWidhtHeigth(packages);    //change column width with column heigth
                }
                else if (tempWidth > tempLength)
                {
                    binWidth = (int)numbersW.Last();
                    binHeigth = (int)numbersL.Last();
                }
                else
                {
                    binWidth = (int)numbersW.Last();
                    binHeigth = (int)numbersH.Last();
                }
            }
            else if (tempHeigth == tempWidth && !(tempWidth == tempLength))
            {
                if (tempLength > tempWidth)
                {
                    binWidth = (int)numbersL.Last();
                    binLength = (int)numbersW.Last();
                    changeWidthLength(packages);      //change column width with column length
                }
                else if (tempLength < tempWidth)
                {
                    binLength = (int)numbersL.Last();
                    binWidth = (int)numbersW.Last();
                }
                binHeigth = (int)numbersH.Last();
            }
            else if (!(tempHeigth == tempWidth) && tempHeigth == tempLength)
            {
                if (tempWidth > tempLength)
                {
                    binWidth = (int)numbersW.Last();
                    binLength = (int)numbersL.Last();
                }
                else if (tempWidth < tempLength)
                {
                    binWidth = (int)numbersL.Last();
                    binLength = (int)numbersW.Last();
                    changeWidthLength(packages);      //change column width with column length
                }
                binHeigth = (int)numbersH.Last();

            }
            else if (!(tempHeigth == tempWidth) && tempWidth == tempLength)
            {
                if (tempWidth > tempHeigth)
                {
                    binLength = (int)numbersH.Last();
                    binHeigth = (int)numbersL.Last();
                    changeLengthHeigth(packages);
                    binWidth = (int)numbersW.Last();
                }
                else if (tempWidth < tempHeigth)
                {
                    binWidth = (int)numbersH.Last();
                    binHeigth = (int)numbersW.Last();
                    changeWidhtHeigth(packages);
                    binLength = (int)numbersL.Last();
                }

            }
            else if (tempHeigth == tempWidth && tempHeigth == tempLength)
            {
                binLength = (int)numbersL.Last();
                binWidth = (int)numbersW.Last();
                binHeigth = (int)numbersH.Last();
            }

            packages.Sort((x, y) => x.packWidth.CompareTo(y.packWidth));
            packages.Reverse();
            truckL = truckLength - binLength;   //עדכון אורך במשאית אחרי חישוב משגרת להזמנה 

            if (truckL >= binLength && truckL > 0)
            {
                //Bins-חישוב מספר ה
                int numberOfBins = truckHeigth / binHeigth;
                printPack = new List<Package>(packages.Count);
                numBinW = truckWidth / binWidth;

                //שליחת נתונים לאלגוריתם בשביל האריזה
                FirstFit(packages, numberOfBins, truckWidth, truckLength, binWidth, binLength, binHeigth);
                binW = false;
                binFrame = false;
            }
            else
            {
                binW = false;
                binFrame = false;
            }
            return packages;
        }

        private void changeLengthHeigth(List<Package> packages)
        {
            var tempVar = 0;
            for (int i = 0; i < packages.Count; i++)
            {
                tempVar = packages[i].packLength;
                packages[i].packLength = packages[i].packHeigth;
                packages[i].packHeigth = tempVar;
            }
        }

        private void changeWidhtHeigth(List<Package> packages)
        {
            var tempVar = 0;
            for (int i = 0; i < packages.Count; i++)
            {
                tempVar = packages[i].packWidth;
                packages[i].packWidth = packages[i].packHeigth;
                packages[i].packHeigth = tempVar;
            }
        }

        private void changeWidthLength(List<Package> packages)
        {
            var tempVar = 0;
            for (int i = 0; i < packages.Count; i++)
            {
                tempVar = packages[i].packWidth;
                packages[i].packWidth = packages[i].packLength;
                packages[i].packLength = tempVar;
            }
        }

        public void FirstFit(List<Package> packages, int bins, int truckWidth, int truckLength, int binWidth, int binLength, int binHeigth)
        {
            //משכפל את רשימת החבילות שיש לארוז
            //ישמש אותנו להחזקת חבילות שעוד לא נכנסו לאריזה,אלה שמכניסים נמחוק מהרשימה
            List<Package> temp_pack = new List<Package>(packages.Count);
            temp_pack.AddRange(packages);

            // Initialize result (Count of bins)
            int res = 0;

            // Create an array to store remaining space in bins by WIDTH there can be at most n bins
            int[] bin_remWidth = new int[packages.Count];

            int temp_bins = bins;

            // Place items one by one
            for (int i = 0; i < packages.Count; i++)
            {
                // Find the first bin that can accommodate packages[i]
                int j;
                for (j = 0; j < res; j++)
                {
                    if (bin_remWidth[j] >= packages[i].packWidth && packages[i].isOccupied != true)
                    {
                        bin_remWidth[j] = bin_remWidth[j] - packages[i].packWidth;
                        packages[i].isOccupied = true; printPack.Add(packages[i]);
                        printPack[printPack.Count() - 1].binNum = j;
                        break;
                    }
                }

                // If no bin could accommodate packages[i]
                if (j == res && res < bins)
                {
                    if (packages[i].isOccupied == false)
                    {
                        bin_remWidth[i] = binWidth - packages[i].packWidth;
                        packages[i].isOccupied = true; printPack.Add(packages[i]);
                        printPack[printPack.Count() - 1].binNum = res;
                    }

                    res++;
                    temp_bins--;

                    if (temp_bins == 0 && binW == false)
                    {
                        bin_remWidth = checkSpace(bin_remWidth, packages, res);
                    }
                    else if (temp_bins == 0 && binFrame == false)
                    {
                        bin_remWidth = checkSpace(bin_remWidth, packages, res);
                    }


                    //אם נשארו עוד חבילות ונגמרו מיכלים אז נפתוח מיכל חדש באותה משגרת של הלקוח
                    if (temp_bins == 0 && packagesEnd(packages, res) == true)
                    {
                        if (temp_pack[res].isOccupied == false)
                        {
                            temp_pack.RemoveRange(0, res);
                        }
                        else
                        {
                            int temp;
                            for (temp = res; temp < packages.Count; temp++)
                            {
                                if (temp_pack[temp].isOccupied == false)
                                {
                                    temp_pack.RemoveRange(0, temp);
                                    break;
                                }
                            }
                        }

                        //שליחת נתונים לאלגוריתם בשביל האריזה
                        if (truckWidth - binWidth >= binWidth && packagesEnd(packages, res) == true)
                        {
                            if (packagesEnd(packages, packages.Count) == true)
                            {
                                break;
                            }

                            for (int frame = 0; frame < temp_pack.Count; frame++)
                            {
                                if (temp_pack[frame].isOccupied == false)
                                {
                                    temp_pack[frame].newBin = temp_pack[frame].newBin + 1;
                                }
                            }
                            binW = true;
                            binFrame = false;
                            FirstFit(temp_pack, bins, truckWidth - binWidth, truckLength, binWidth, binLength, binHeigth);
                        }
                        else if (truckLength - binLength >= binLength && packagesEnd(packages, res) == true)
                        {
                            truckWidth = truckWidth + binWidth * (printPack[i].newBin + 1);
                            if (packagesEnd(packages, packages.Count) == true)
                            {
                                break;
                            }

                            for (int frame = 0; frame < temp_pack.Count; frame++)
                            {
                                if (temp_pack[frame].isOccupied == false)
                                {
                                    temp_pack[frame].newFrame = printPack[i].newFrame + 1;
                                    temp_pack[frame].newBin = 1;
                                }
                            }

                            binFrame = true;
                            binW = false;
                            FirstFit(temp_pack, bins, truckWidth, truckLength - binLength, binWidth, binLength, binHeigth);
                        }
                        else if ((packagesEnd(packages, res) == true))   //אם יש לנו עוד חבילות שלא הכנסו ונבדוק אם יש עוד מקום במשאית
                        {
                            if (truckWidth - binWidth < binWidth && truckLength - binLength < binLength)
                            {
                                binW = false;
                                binFrame = false;
                                break;
                            }
                            break;
                        }

                        if (packagesEnd(packages, res) == false)
                        {
                            binW = false;
                            binFrame = false;
                            break;
                        }
                    }
                    else if (packagesEnd(packages, res) == false)
                    {
                        binW = false;
                        binFrame = false;
                        break;
                    }
                }
            }
        }

        public static int[] checkSpace(int[] bin_remWidth, List<Package> packages, int res)
        {
            int temp = res;
            for (int i = 0; i < packages.Count; i++)
            {
                for (int j = 0; j < packages.Count; j++)
                {
                    if (bin_remWidth[i] == 0)
                    {
                        break;
                    }
                    else if (bin_remWidth[i] >= packages[j].packWidth && packages[j].isOccupied != true)
                    {
                        bin_remWidth[i] = bin_remWidth[i] - packages[j].packWidth;
                        packages[j].isOccupied = true;
                        packages[j].newBin = packages[i].newBin;
                        packages[j].newFrame = packages[i].newFrame;
                        printPack.Add(packages[j]);
                        printPack[temp].binNum = packages[i].binNum;

                        temp++;
                        if (bin_remWidth[i] > 0)
                        {
                            bin_remWidth = checkSpace(bin_remWidth, packages, res);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            return bin_remWidth;
        }

        public static Boolean packagesEnd(List<Package> packages, int res)    //בודק אם יש עוד חבילות ברשימה
        {
            Boolean temp = false;

            for (int i = res; i < packages.Count; i++)
            {
                if (packages[i].isOccupied == false)   // אם ברשימה באינדקס הבא יש עוד חבילה שלא ארזנו אותה אז נחזיר אמת
                {
                    temp = true;
                    break;
                }
            }
            return temp;
        }

        private GoodSupplyEntities db = new GoodSupplyEntities();

        // GET: BinPacking
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SetByCity(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var supplier = db.Suppliers.Where(s => s.Id == id).FirstOrDefault();

            var mySupplierOrders = db.Orders.Where(o => o.SupplierId == supplier.Id && o.SupplierApproval == 1).Include(o => o.Clients).Include(o => o.Suppliers);
            return View(mySupplierOrders.OrderBy(o => o.Clients.BusinessAddress).ToList());
        }

        [HttpPost]
        public ActionResult SetOrdersPosition(ICollection<BinPackingAlgViewModel> binPackingAlgVM)
        {
            List<Package> packgesList = new List<Package>();
            List<Package> tempList = new List<Package>();

            for (var i = 1; i <= binPackingAlgVM.Count(); i++)
            {
                var currentOrderId = binPackingAlgVM.Where(b => b.Position == i).FirstOrDefault().Id;
                var orderDetails = db.OrderDetails.Where(o => o.OrderId == currentOrderId).ToList();

                packages = new List<Package>(orderDetails.Count());

                foreach (var item in orderDetails)
                {
                    Package package = new Package { orderId = item.OrderId, CatalogNum = item.ManufacturerProducts.CatalogNumber, packWidth = item.ManufacturerProducts.Width.Value, packLength = item.ManufacturerProducts.Length.Value, packHeigth = item.ManufacturerProducts.Height.Value };

                    for (int j = 0; j < item.Quantity; j++)
                    {
                        packages.Add(new Package(package.packWidth, package.packLength, package.packHeigth, package.isOccupied, package.binNum, package.newBin, package.newFrame, package.CatalogNum, package.orderId));
                    }
                }

                tempList = Packer(packages, truckWidth, truckLength, truckHeigth);
                packgesList.AddRange(tempList);
                truckLength = truckLength - packages[0].packLength;  // עדכון גודל משאית להזמנה הבאה 
            }

            IEnumerable<Package> _Package = packgesList as IEnumerable<Package>;
            return View(_Package);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}