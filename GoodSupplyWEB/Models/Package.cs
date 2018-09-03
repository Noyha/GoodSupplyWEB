using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodSupplyWEB.Models
{
    public class Package
    {
        public IEnumerable<Package> Packages { get; set; }

        public int packWidth;
        public int packLength { get; set; }
        public int packHeigth;
        public bool isOccupied = false;
        public int binNum;
        public int newBin = 1;
        public int newFrame = 1;
        public string CatalogNum;
        public int orderId;

        public Package(int packWidth, int packLength, int packHeigth, bool isOccupied, int binNum, int newBin, int newFrame, string CatalogNum, int orderId)
        {
            this.packWidth = packWidth;
            this.packLength = packLength;
            this.packHeigth = packHeigth;
            this.isOccupied = isOccupied;
            this.binNum = binNum;
            this.newBin = newBin;
            this.newFrame = newFrame;
            this.CatalogNum = CatalogNum;
            this.orderId = orderId;
        }

        public Package() {}
    }
}