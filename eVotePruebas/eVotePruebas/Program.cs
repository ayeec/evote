/*
 * Created by SharpDevelop.
 * User: Alejandro
 * Date: 25/01/2012
 * Time: 05:25 p.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.Xml;
using Community.CsharpSqlite;

namespace eVotePruebas
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
        public static Sqlite3.sqlite3 db;
        public const string dbase = "votos.db";
        /// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
            int stat;
            string a = "";
            System.IO.File.Delete(dbase);
            if (!System.IO.File.Exists(dbase))
            {

                stat = Sqlite3.sqlite3_open(dbase, out db);
                stat = Sqlite3.sqlite3_exec(db, eVotePruebas.Properties.Resources.tablapr, null, null, ref a);
                Sqlite3.sqlite3_exec(db, eVotePruebas.Properties.Resources.tablacan, null, null, ref a);
            }
            else
            {
                stat = Sqlite3.sqlite3_open(dbase, out db);
            }

            if (stat != Sqlite3.SQLITE_OK)
            {
                MessageBox.Show(a);
                Environment.Exit(0);
            }
            /*aassd*/
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Progresivo());
		}
	}
}
