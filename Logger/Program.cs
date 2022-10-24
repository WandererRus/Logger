using System.Text;
using System.Xml.Linq;

namespace Logger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Loger loger = new Loger();
            try
            {
                loger.SetStream(new StreamWriter("log.txt", true, Encoding.UTF8));
                Teams teams = new Teams();
                teams.NewWorker("Петя", loger);
                Thread.Sleep(1000);
                teams.NewWorker("Коля", loger);
                Thread.Sleep(1000);
                teams.NewWorker("Ваня", loger);

                Console.WriteLine(teams[2]);
                
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally 
            {
                if (loger != null)
                {
                    loger.SaveLog();
                    loger.WriteLog("Логирование закончено в " + DateTime.Now.ToString());
                    loger.Close();
                }
            }
            
        }
    }
    class Worker
    {
        public string? Name { get; set; }

        public Worker (string? name, Loger loger)
        {
            Name = name;
            loger.WriteLog(DateTime.Now.ToString() + "В город приехал" + Name);
        }
    }
    class Teams
    {
        List<Worker> workers = new List<Worker>();

        public void NewWorker (string? Name, Loger loger)
        {
            workers.Add(new Worker(Name,loger));
            loger.WriteLog(DateTime.Now.ToString() + "В команде добавили" + Name );
        }

        public string this[int a]
        {
            get 
            {
                if (a >= 0 && a < workers.Count)
                { return workers[a].Name; }
                else { return ""; }
            }
            set 
            {
                if (a >= 0 && a < workers.Count)
                {  
                    workers[a].Name = value;                 
                }
                else {  }
            }
        }
    }

    class Loger
    {
        StreamWriter? _sw;
        List<string> _list = new List<string>();

        public Loger()
        {
           
        }

        public void WriteLog(string str)
        {
            _list.Add(str); 
        }
        public void SaveLog()
        {
            if (_sw != null)
            {
                foreach (string text in _list)
                {                
                        _sw.WriteLine(text);  
                }
            }
        }
        public void SetStream(StreamWriter sw)
        {
            _sw = sw;
        }
        public void Close()
        {
            if (_sw != null)
            {
                _sw.Close();
            }
        }
    }


}