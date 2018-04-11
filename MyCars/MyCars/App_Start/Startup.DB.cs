using MyCars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCars
{
	public partial class Startup
	{
	public void InitDB()
        {
            using (var context = new ApplicationDbContext())
            {
                if (context.Brands.Count() == 0)
                {
                    var brand = new Brand();
                    brand.Name = "Audi";
                    context.Brands.Add(brand);
                    context.SaveChanges();

                    brand = new Brand();
                    brand.Name = "BMW";
                    context.Brands.Add(brand);
                    context.SaveChanges();

                    brand = new Brand();
                    brand.Name = "Chevrolet";
                    context.Brands.Add(brand);
                    context.SaveChanges();

                    brand = new Brand();
                    brand.Name = "Mercedes-Benz";
                    context.Brands.Add(brand);
                    context.SaveChanges();

                    brand = new Brand();
                    brand.Name = "Toyota";
                    context.Brands.Add(brand);
                    context.SaveChanges();
                }

                if (context.Types.Count() == 0)
                {
                    var model = new TypeModel();
                    string[] type1 = new string[] { "A2", "A4", "A5", "A6", "A8", "Q7", "R8", "RS4", "TT" };
                    string[] type2 = new string[] { "02 (E10)", "3er 328", "5er 525", "7er 750", "M3", "M5", "M6", "X5", "X6", "Z4" };
                    string[] type3 = new string[] { "Camaro", "Cobalt", "Colorado", "Impala"};
                    string[] type4 = new string[] { "Coupe", "280", "500", "A-Class 210", "C-Class 220", "C-Class 240", "C-Class 63 AMG", "CL-Class 63 AMG" };
                    string[] type5 = new string[] { "Brevis", "Camry", "Carina", "Carina E", "Corolla", "Corolla Ceres", "Corolla FX", "Corolla Verso", "Land Cruiser", "Land Cruiser (90) Prado" };

                    for (int i = 0; i < type1.Length; i++)
                    {
                        model.Name = type1[i];
                        model.BrandId = 1;
                        context.Types.Add(model);
                        context.SaveChanges();
                    }

                    for (int i = 0; i < type2.Length; i++)
                    {
                        model.Name = type2[i];
                        model.BrandId = 2;
                        context.Types.Add(model);
                        context.SaveChanges();
                    }

                    for (int i = 0; i < type3.Length; i++)
                    {
                        model.Name = type3[i];
                        model.BrandId = 3;
                        context.Types.Add(model);
                        context.SaveChanges();
                    }

                    for (int i = 0; i < type4.Length; i++)
                    {
                        model.Name = type4[i];
                        model.BrandId = 4;
                        context.Types.Add(model);
                        context.SaveChanges();
                    }

                    for (int i = 0; i < type5.Length; i++)
                    {
                        model.Name = type5[i];
                        model.BrandId = 5;
                        context.Types.Add(model);
                        context.SaveChanges();
                    }
                }
            }
        }
	}
}