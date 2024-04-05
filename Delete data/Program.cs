using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Delete_data
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program pr = new Program();
            while (true)
            {
                try
                {
                    Console.Write("\nketik K untuk terhubung ke database atau E untuk keluar dari aplikasi: ");
                    char chr = Convert.ToChar(Console.ReadLine());
                    switch (chr)
                    {
                        case 'K':
                            {
                                Console.Clear();
                                Console.WriteLine("Masukan database yang dituju kemudian klik enter:");
                                string db = Console.ReadLine();
                                SqlConnection conn = null;
                                string strkoneksi = "data source = METEORITE\\SQL2019; " +
                                    "initial catalog = Pacarjadi2; " +
                                    "User ID=sa; password = meteorite";
                                conn = new SqlConnection(string.Format(strkoneksi, db));
                                conn.Open();
                                Console.Clear();
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nMenu");
                                        Console.WriteLine("1. Melihat Seluruh Data");
                                        Console.WriteLine("2. Tambah Data");
                                        Console.WriteLine("3. Hapus Data");
                                        Console.WriteLine("4. Keluar");
                                        Console.WriteLine("\nEnter your choice (1-4): ");
                                        char ch = Convert.ToChar(Console.ReadLine());
                                        switch (ch)
                                        {
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Data Pacar & Mantan Jadi\n");
                                                    Console.WriteLine();
                                                    pr.baca(conn);
                                                }
                                                break;
                                            case '2':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Input Data Pacar & Mantan Jadi\n");
                                                    Console.WriteLine("Masukkan Nama Pacar / Mantan :");
                                                    string NmaPcr = Console.ReadLine();
                                                    Console.WriteLine("Masukkan No Telepon Pacar / Mantan :");
                                                    string notlpn = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Alamat Pacar / Mantan :");
                                                    string Almt = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Jenis Kelamin (L/P) :");
                                                    string jk = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Status Hubungan Saat Ini (Putus/Masih) :");
                                                    string sts = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Masa Hubungan :");
                                                    string masa = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.insert(NmaPcr, notlpn, Almt, jk, sts, masa, conn);
                                                    }
                                                    catch (Exception e)
                                                    {
                                                        Console.WriteLine("\nAnda tidak memiliki " + "akses untuk menambah data atau menambah data atau data yang anda masukan salah");
                                                        Console.WriteLine(e.ToString());
                                                    }
                                                }
                                                break;
                                            case '3':
                                                {
                                                    Console.Clear ();
                                                    Console.WriteLine("Masukan nama pacar / mantan yang ingin dihapus:\n");
                                                    string nmaPcr = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.delete(nmaPcr, conn);
                                                    }
                                                    catch (Exception e)
                                                    {
                                                        Console.WriteLine("\nanda tidak memiliki" + "akses untuk menghapus data ");
                                                        Console.WriteLine(e.ToString());
                                                    }
                                                }
                                                break;
                                            case '4':
                                                conn.Close();
                                                Console.Clear();
                                                Main(new string[0]);
                                                return;
                                            default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("\ninvalid option");
                                                }
                                                break;
                                        }
                                    }
                                    catch
                                    {
                                        Console.Clear();
                                        Console.WriteLine("\ncheck for the values entered.");
                                    }
                                }
                            }
                        case 'E':
                            return;
                        default:
                            {
                                Console.WriteLine("\ninvalid option");
                            }
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("tidak dapat mengakses database tersebut\n");
                    Console.ResetColor();
                }
            }
        }
        public void baca(SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand("SELECT NmPCR, NoTlpn, AlmtPCR, JK, STSHBG, MASAHBG FROM DataPacar", conn);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                for (int i = 0; i < r.FieldCount; i++)
                {
                    Console.WriteLine(r.GetValue(i));
                }
                Console.WriteLine();
            }
            r.Close();
        }
        public void insert(string Nma, string notlpn, string Almt, string jk, string shb, string mhb, SqlConnection conn)
        {
            string str = "";
            str = "insert into DataPacar (NmPcr,NoTlpn,AlmtPcr,JK,STSHBG,MASAHBG) " + "values(@nm,@nt,@a,@jk,@sb,@mb";
            SqlCommand cmd = new SqlCommand(str, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("nm", Nma));
            cmd.Parameters.Add(new SqlParameter("nt", notlpn));
            cmd.Parameters.Add(new SqlParameter("a", Almt));
            cmd.Parameters.Add(new SqlParameter("jk", jk));
            cmd.Parameters.Add(new SqlParameter("sb", shb));
            cmd.Parameters.Add(new SqlParameter("mb", mhb));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data berhasil ditambahkan");
        }
        public void delete(string Nma, SqlConnection conn)
        {
            string str = "";
            str = "Delete DataPacar where NmPcr = @nm";
            SqlCommand cmd = new SqlCommand(str, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("nm", Nma));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data berhasil dihapus");
        }
    }
}
