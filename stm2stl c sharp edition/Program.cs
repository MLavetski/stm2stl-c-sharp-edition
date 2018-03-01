using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stm2stl_c_sharp_edition
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        public static T[] RemoveAt<T>(this T[] source, int index, int amount)
        {
            T[] dest = new T[source.Length - amount];
            if (index > 0)
                Array.Copy(source, 0, dest, 0, index);

            if (index < source.Length - amount)
                Array.Copy(source, index + amount, dest, index, source.Length - index - amount);

            return dest;
        }

    }
}
