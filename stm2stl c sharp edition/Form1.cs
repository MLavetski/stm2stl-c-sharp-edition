using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stm2stl_c_sharp_edition
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void exitBut_Click(object sender, EventArgs e)
        {
            Close();
        }

        float[][] dataMuliplied;
        bool[] wasFixed;
        ushort[] Nx, Ny;
        char[][] fieldName,
                 scaleXYname,
                 scaleZname;
        float[] scaleX, scaleY, scaleZ;
        ushort field_amount;
        double a, b, c;
        
        private void stlBut_Click(object sender, EventArgs e)
        {
            //We have an array of data witch can be repressented as matrix Ny by Nx
            //that function takes points in said matrix in specified order(points MUST be CCW) to represent
            //verticles of triangles, stores them in vector of triangle class objects, calls calcNormal function
            //for each triange, and writes them into binary .stl file
            //first, generate filemane for output file.
            SaveFileDialog SFD = new SaveFileDialog();
            //generation of outputfilename. we take an input file name w/o extension and add there a string witch represents a field we took from source file.
            SFD.FileName = Path.GetFileNameWithoutExtension(openFileDialog1.FileName) + "_" + new string(fieldName[fieldSelector.SelectedIndex]);
            SFD.DefaultExt = ".stl";
            SFD.Filter = "Stereolithography file (*.stl)|*.stl";
            //then, we open the file with generatated name. If it doesn't exist, create it.
            if (SFD.ShowDialog() == DialogResult.OK)
            {
                BinaryWriter bw = new BinaryWriter(File.Open(SFD.FileName, FileMode.Create));
                int index = fieldSelector.SelectedIndex;//ui->fieldselect->currentIndex();
                ushort sizeX = Nx[index];
                ushort sizeY = Ny[index];
                char[] header = new char[80];
                for (int j = 0; j < 80; j++)
                {
                    header[j] = (char)0;
                }
                int amountTriangles = (sizeX - 1) * (sizeY - 1) * 2;
                triangle[] triangles = new triangle[amountTriangles];
                for(int j=0;j<amountTriangles;j++)
                {
                    triangles[j] = new triangle();
                }
                int i = 0;//counter for current triangle
                float scaleMult = 1000;//value by witch we will divide the x and y coordinates of our points, since most software uses our values as mm instead of mkm.
                float scaleXfixed = scaleX[index] / scaleMult, scaleYfixed = scaleY[index] / scaleMult;
                for (int y = 0; y < sizeY - 1; y++)  //write coordinates into corresponding triangle
                {                           //there are to types of triangles
                    for (int x = 0; x < sizeX; x++)//flat side up and down
                    {
                        if (x != 0)//here we writing flat side down triangle
                        {
                            triangles[i].point3[0] = x * scaleXfixed;
                            triangles[i].point3[1] = y * scaleYfixed;
                            triangles[i].point3[2] = dataMuliplied[index][y * Nx[index] + x] / scaleMult;
                            triangles[i].point2[0] = (x - 1) * scaleXfixed;
                            triangles[i].point2[1] = (y + 1) * scaleYfixed;
                            triangles[i].point2[2] = dataMuliplied[index][(y + 1) * Nx[index] + (x - 1)] / scaleMult;
                            triangles[i].point1[0] = x * scaleXfixed;
                            triangles[i].point1[1] = (y + 1) * scaleYfixed;
                            triangles[i].point1[2] = dataMuliplied[index][(y + 1) * Nx[index] + x] / scaleMult;
                            i++;
                        }
                        if (x != sizeX - 1)//here we writing flat side up triangle
                        {
                            triangles[i].point3[0] = x * scaleXfixed;
                            triangles[i].point3[1] = y * scaleYfixed;
                            triangles[i].point3[2] = dataMuliplied[index][y * Nx[index] + x] / scaleMult;
                            triangles[i].point2[0] = x * scaleXfixed;
                            triangles[i].point2[1] = (y + 1) * scaleYfixed;
                            triangles[i].point2[2] = dataMuliplied[index][(y + 1) * Nx[index] + x] / scaleMult;
                            triangles[i].point1[0] = (x + 1) * scaleXfixed;
                            triangles[i].point1[1] = y * scaleYfixed;
                            triangles[i].point1[2] = dataMuliplied[index][y * Nx[index] + (x + 1)] / scaleMult;
                            i++;
                        }
                    }
                }
                for (int j = 0; j < amountTriangles; j++)
                {
                    triangles[j].calcNorm();
                }

                //ok, we got triangles ready, now write them into .stl file
                // we will serialize the data into the file
            //bw.setByteOrder(QDataStream::LittleEndian); dunno how to set byte order to LE with BinaryWriter class yet. will check if needed
            //write 80-byte header into file. In our case, header is filled with empty symbols.
            bw.Write(header);
            //write int with amount of triangles in file
            bw.Write(amountTriangles);
                //write each triangle
                for (int j = 0; j < amountTriangles; j++)
                {
                    //first - 3 dimensions of normal vector
                    bw.Write(triangles[j].normal[0]);
                    bw.Write(triangles[j].normal[1]);
                    bw.Write(triangles[j].normal[2]);
                    //3 dim. of first point
                    bw.Write(triangles[j].point1[0]);
                    bw.Write(triangles[j].point1[1]);
                    bw.Write(triangles[j].point1[2]);
                    //second point
                    bw.Write(triangles[j].point2[0]);
                    bw.Write(triangles[j].point2[1]);
                    bw.Write(triangles[j].point2[2]);
                    //third point
                    bw.Write(triangles[j].point3[0]);
                    bw.Write(triangles[j].point3[1]);
                    bw.Write(triangles[j].point3[2]);
                    //and the attribyte file count, witch, by default, is zero.
                    bw.Write(triangles[j].attrByteCount);
                }
                //then we close the output file
                bw.Close();
            }
        }

        private void fixBut_Click(object sender, EventArgs e)
        {
            int ind = fieldSelector.SelectedIndex;
            ushort X = Nx[ind], Y = Ny[ind];
            for (int i = 0; i < X * Y; i += X)//for each row
            {
                calcCurve(i);
                for (int j = 0; j < X; j++)
                {
                    dataMuliplied[ind][i + j] -= (float)((a * j * j) + (b * j) + c);
                }
            }
            wasFixed[ind] = true;
            fixBut.Enabled = false;
        }

        private void calcCurve(int start)
        {
            int ind = fieldSelector.SelectedIndex;
            ushort X = Nx[ind];
            int[] xArray = new int[X];
            float[] yArray = new float[X];
            for (int i = 0; i < X; i++)
            {
                xArray[i] = i;
                yArray[i] = dataMuliplied[ind][start + i];
            }
            double[] ATAbase = new double[5], ATYmatrix = new double[3], CBAmatrix = new double[3]; 
            double[][] ATAmatrix = new double[3][], ATAinversed = new double[3][];
            ATAmatrix[0] = new double[3]; ATAmatrix[1] = new double[3]; ATAmatrix[2] = new double[3];
            ATAinversed[0] = new double[3]; ATAinversed[1] = new double[3]; ATAinversed[2] = new double[3];
            double det = 0;
            for(int i = 0; i<5;i++)
            {
                ATAbase[i]=0;
                for(int j = 0; j<X;j++)
                {
                    ATAbase[i] += Math.Pow(xArray[j], i);
                }
            }
            for(int i = 0; i<3;i++)
            {
                CBAmatrix[i]=0;
                for(int j = 0; j<3;j++)
                {
                    ATAmatrix[i][j] = ATAbase[i + j];
                }
            }
            //calculate determinant
            for(int i = 0; i< 3; i++)
                det = det + (ATAmatrix[0][i] * (ATAmatrix[1][(i + 1) % 3] * ATAmatrix[2][(i + 2)%3] - ATAmatrix[1][(i + 2) % 3] * ATAmatrix[2][(i + 1)%3]));
            //calculate inverse matrix
            for(int i = 0; i< 3; i++)
            {
                for(int j = 0; j< 3; j++)
                    ATAinversed[i][j]=((ATAmatrix[(j + 1) % 3][(i + 1) % 3] * ATAmatrix[(j + 2) % 3][(i + 2)%3]) - (ATAmatrix[(j + 1) % 3][(i + 2) % 3] * ATAmatrix[(j + 2) % 3][(i + 1)%3]))/det;
            }
            for(int i = 0; i<3;i++)
            {
                ATYmatrix[i]=0;
                for(int j = 0; j<X;j++)
                {
                    ATYmatrix[i]+=(Math.Pow(xArray[j], i)* yArray[j]);
                }
            }
            for(int j = 0; j< 3; ++j)
                for(int k = 0; k< 3; ++k)
                {
                    CBAmatrix[j] += ATYmatrix[k] * ATAinversed[k][j];
                }
            c = CBAmatrix[0]; b = CBAmatrix[1]; a = CBAmatrix[2];
        }
        private void openFileBut_Click(object sender, EventArgs e)
        {
            fieldSelector.Enabled = false;
            fixBut.Enabled = false;
            stlBut.Enabled = false;
            openFileDialog1.Filter = "spm File|*.spm";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fieldSelector.Items.Clear();
                inputFilenameBox.Text = openFileDialog1.FileName;
                BinaryReader sr = new
                   BinaryReader(File.Open(openFileDialog1.FileName, FileMode.Open));

                byte spmType = sr.ReadByte();//Getting filetype.
                byte[] HEAD_SPM1 = sr.ReadBytes(223); //Getting HEAD_SPM1 from file.
                ushort MaxNx, MaxNy, what_this;
                field_amount = 0;
                MaxNx = BitConverter.ToUInt16(HEAD_SPM1, 49);
                MaxNy = BitConverter.ToUInt16(HEAD_SPM1, 51);
                what_this = BitConverter.ToUInt16(HEAD_SPM1, 59);
                while (what_this!=0)//Subfield what_this contains info about stored fields witch we can use to find amount of those.
                {
                    field_amount++;
                    what_this &= (ushort)(what_this - 1); //current AND itself-1 to remove the least bit. repeat untill its 0.
                }
                byte[][] dataRaw = new byte[field_amount][];//Place for raw data of fields from spm1 files. 2 bytes for each point.
                short[][] dataShort = new short[field_amount][];//Same data but transformed into 16 bit integers.
                dataMuliplied = new float[field_amount][];//Data after applying z-multiplier.
                wasFixed = new bool[field_amount];
                byte[][] notifications = new byte[field_amount][];
                Nx = new ushort[field_amount]; Ny = new ushort[field_amount];
                fieldName = new char[field_amount][];
                scaleXYname = new char[field_amount][];
                scaleZname = new char[field_amount][];
                scaleX = new float[field_amount]; scaleY = new float[field_amount]; scaleZ = new float[field_amount];

                for (ushort i = 0; i < field_amount; i++)//Getting all notifications about stored fields of data.
                {
                    notifications[i] = sr.ReadBytes(336);
                    fieldName[i] = new char[32];
                    scaleXYname[i] = new char[6];
                    scaleZname[i] = new char[6];
                }

                for (ushort i = 0; i < field_amount; i++)//Getting that information.
                {
                    Array.Copy(notifications[i],0,fieldName[i],0,32);
                    Array.Copy(notifications[i], 68, scaleXYname[i], 0, 6);
                    Array.Copy(notifications[i], 74, scaleZname[i], 0, 6);
                    Nx[i] = BitConverter.ToUInt16(notifications[i], 34);
                    Ny[i] = BitConverter.ToUInt16(notifications[i], 36);
                    scaleX[i] = BitConverter.ToSingle(notifications[i], 40);
                    scaleY[i] = BitConverter.ToSingle(notifications[i], 44);
                    scaleZ[i] = BitConverter.ToSingle(notifications[i], 48);

                    dataRaw[i] = new byte[Nx[i] * Ny[i] * 2];
                    dataShort[i] = new short[Nx[i] * Ny[i]];
                    dataMuliplied[i] = new float[Nx[i] * Ny[i]];
                }

                //Getting all raw data
                for (ushort i = 0; i < field_amount; i++)
                {
                    for (int j = 0; j < (Nx[i] * Ny[i] * 2); j++)
                    {
                        dataRaw[i][j] = sr.ReadByte();
                    }
                }
                for (int i = 0; i < field_amount; i++)//Trasform raw data into 16bit integers.
                {
                    for (int j = 0; j < (Nx[i] * Ny[i]); j++)
                    {
                        dataShort[i][j] = BitConverter.ToInt16(dataRaw[i],j*2);
                    }
                }
                for (int i = 0; i < field_amount; i++)//Multiping data by z-multiplier
                {
                    for (int j = 0; j < (Nx[i] * Ny[i]); j++)
                    {
                        dataMuliplied[i][j] = dataShort[i][j] * scaleZ[i];
                    }
                }
                sr.Close();
                for(ushort i=0;i<field_amount;i++)
                {
                    fieldSelector.Items.Add(new string(fieldName[i]));
                }
                fieldSelector.Enabled = true;
                fieldSelector.SelectedIndex = 0;
                fixBut.Enabled = true;
                stlBut.Enabled = true;
                delZeros();
            }
        }

        private void delZeros()
        {
            ushort deleted = 0;
            bool[][] isMT = new bool[field_amount][];
            for(int i = 0;i<isMT.Length;i++)
            {
                isMT[i] = new bool[Ny[i]];
                for(int j=0;j<isMT[i].Length;j++)
                    isMT[i][j] = true;
            }
            for (int i = 0; i < isMT.Length; i++)
            {
                for (int j = 0; j < Ny[i]; j++)
                {
                    for (int k = 0; k<Nx[i]; k++)
                    {
                        if(dataMuliplied[i][j*Nx[i]+k]!=0)
                        {
                            isMT[i][j] = false;
                            break;
                        }
                    }
                    if(isMT[i][j])
                    {
                        dataMuliplied[i] = dataMuliplied[i].RemoveAt(j * Nx[i], Nx[i]);
                        j--;
                        Ny[i]--;
                        deleted++;
                    }
                }
            }
            if (deleted > 0)
            {
                MessageBox.Show($"deleted {deleted} empty strings");
            }
        }

        private void fieldSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            fixBut.Enabled = !wasFixed[fieldSelector.SelectedIndex];
        }

    }

    public class triangle
    {
        public float[] point1 = new float[3], point2 = new float[3], point3 = new float[3], normal = new float[3];//1st is x; 2nd is y; 3rd is z.
        public ushort attrByteCount = 0;
        public void calcNorm()
        {
            normal[0] = (point1[1] * (point2[2] - point3[2]) + point2[1] * (point3[2] - point1[2]) + point3[1] * (point1[2] - point2[2]));
            normal[1] = (point1[2] * (point2[0] - point3[0]) + point2[2] * (point3[0] - point1[0]) + point3[2] * (point1[0] - point2[0]));
            normal[2] = (point1[0] * (point2[1] - point3[1]) + point2[0] * (point3[1] - point1[1]) + point3[0] * (point1[1] - point2[1]));
        }
    }
}
