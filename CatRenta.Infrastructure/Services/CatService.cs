using CatRenta.Application.Interfaces;
using CatRenta.Domain;
using CatRenta.EFData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CatRenta.Infrastructure.Services
{
    public class CatService : ICatService
    {
        private readonly DataContext _context;
        public CatService()
        {
            _context = new DataContext();
        }

        public bool CanselAsyncMethod { get; set; }

        public event InsertCatDelegate EventInsertItem;

        public int Count()
        {
            return _context.Cats.Count();
        }

        public void InsertCats(int count, ManualResetEvent mrse)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            for (int i = 0; i < count; i++)
            {
                mrse.WaitOne();
                if (CanselAsyncMethod)
                {
                    CanselAsyncMethod = false;
                    break;
                }
                AppCat appCat = new AppCat
                {
                    Name = "Name" + i,
                    Birthday = DateTime.Now,
                    Details = "asdfaf",
                    Gender = true,
                    Image = "Image"
                };
                _context.Cats.Add(appCat);
                _context.SaveChanges();
                //if (EventInsertItem != null)
                //    EventInsertItem(i+1);
                EventInsertItem?.Invoke(i + 1);
                Debug.WriteLine("Insert cat " + appCat.Id);
            };

            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Debug.WriteLine("Час додавання котів: " + elapsedTime);
        }

        public void InsertCats(int count)
        {
            throw new NotImplementedException();
        }

        public Task InsertCatsAsync(int count, ManualResetEvent mrse)
        {
            return Task.Run(() => InsertCats(count, mrse));
            //return Task.Run(() => { int i = 1 + 1;  });
        }
    }
}
