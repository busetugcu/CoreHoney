using CoreHoney.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreHoney.DataAccess.Concrete.EfCore
{
    public class SeedDatabase
    {

        public static void Seed()
        {
            var context = new HoneyContext();
            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                if (context.Honeys.Count() == 0)
                {
                    context.Honeys.AddRange(Honeys);
                }

                context.SaveChanges();

            }

        }

        private static Honey[] Honeys =
        {
             new Honey() { Name="Çiçek Balı",Decription="850gr Çiçek Balı, Organik Sertifikalı, ülkemizin verimli topraklarından olan İç Anadolu bölgesinde üretilen, kendine has aroması ve lezzeti ile günlük kullanıma uygun olan bal çeşitlerimizdendir.",Price=350,Image="1.jpeg" },
              new Honey() { Name="Organik Oğul Balı ",Decription="450 gr Oğul arı, koloni olarak yaşayan mevcut arılardan baharda ayrılan genç kraliçe ve genç işci arılar tarafından oluşturulan yeni arı kolonisine denir. Arı kolonilerinin çoğalması oğul arılar ile olur. Oğul arı kolonisinden ilk yıl hasat edilen bala \"Oğul Balı\" denir.",Price=1123,Image="2.jpeg" },
               new Honey() { Name="Organik Çiçek Balı",Decription="225 gr 900m ile 1200m yükseklikte bulunan yayla ve kırlarda üretilmiş organik baldır. Kahvaltılarınız için tercih edebileceğiniz lezzetli ve şifalı organik çiçek balıdır.",Price=100,Image="3.jpeg" },
                new Honey() { Name="Karakovan Petek Bal",Decription="1.1kg ,Temel petek eklemesi yapılmadan tamamen arılar tarafından üretildiği için Karakovan balı diyoruz. Eğriçayır Organik Karakovan Balımız 2017 Apimondia Arıcılık yarışmasında altın madalya ile ödüllendirilmiştir.",Price=453,Image="4.jpeg" },
                 new Honey() { Name="OrganikPetek Bal",Decription="400 gr ,Eğer kaynağını bildiğiniz, iyi bir Organik Petek Bal arıyorsanız, Eğriçayır güvencesiyle, Organik Petek Balımızı denemenizi tavsiye ederiz. Petek Balımız Organik Sertifikalı olup kimyasal kalıntı içermez.",Price=174,Image="5.jpeg" },
                  new Honey() { Name="Organik Sedir Balı",Decription="300griEğer kaynağını bildiğiniz, iyi bir Organik Petek Bal arıyorsanız, Eğriçayır güvencesiyle, Organik Petek Balımızı denemenizi tavsiye ederiz. Petek Balımız Organik Sertifikalı olup kimyasal kalıntı içermez.",Price=166,Image="6.jpeg" },
        };




    }
}
