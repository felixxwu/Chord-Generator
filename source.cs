using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Chord_Generator
{
    public partial class Form1 : Form
    {
        Random random = new Random();

        int FreeBeats;
        int[,] AllChords = new int[8, 4];
        string Library = Application.StartupPath + "/Library/";

        public Form1()
        {
            if (!CorrectFiles())
            {
                MessageBox.Show("The folder 'Library' was not found, or some files were missing. The application will now close.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            InitializeComponent();
            FormClosing += Form1_FormClosing;
        }

        private bool CorrectFiles()
        {
            if (!Directory.Exists(Library)) return false;
            for (int i = 0; i < 24; i++)
            {
                if (!File.Exists(Library + i + ".wav")) return false;
            }
            if (!File.Exists(Library + "click.wav")) return false;
            if (!File.Exists(Library + "clickhigh.wav")) return false;
            if (!File.Exists(Library + "favourites.txt")) return false;
            if (!File.Exists(Library + "reference.mid")) return false;
            return true;
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult ExitDialogue = MessageBox.Show("Progress will be lost, are you sure you want to exit?",
                "Exit Application", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            
            if (ExitDialogue == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            trackBar1_Scroll(sender, e);
            trackBar2_Scroll(sender, e);
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    AllChords[i, j] = -1;
                }
                UpdateFavLabels(i);
            }
            UpdateNotes();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                Box(i).Visible = false;
            }
            for (int i = 0; i < trackBar1.Value + 1; i++)
            {
                Box(i).Visible = true;
                BarSlider(i).Maximum = 16 - trackBar1.Value * 2 + trackBar1.Value;
                BarCount(i).Text = Convert.ToString(BarSlider(i).Value);
            }
            UpdateFreeBeats(-1);
        }

        #region UI Item returns

        private GroupBox Box(int BoxNumber)
        {
            switch (BoxNumber)
            {
                case 0: return Box0;
                case 1: return Box1;
                case 2: return Box2;
                case 3: return Box3;
                case 4: return Box4;
                case 5: return Box5;
                case 6: return Box6;
                case 7: return Box7;
            }
            return null;
        }

        private Button RandNewButton(int ButtonNumber)
        {
            switch (ButtonNumber)
            {
                case 0: return RandNew0;
                case 1: return RandNew1;
                case 2: return RandNew2;
                case 3: return RandNew3;
                case 4: return RandNew4;
                case 5: return RandNew5;
                case 6: return RandNew6;
                case 7: return RandNew7;
            }
            return null;
        }

        private Button PlayButton(int ButtonNumber)
        {
            switch (ButtonNumber)
            {
                case 0: return Play0;
                case 1: return Play1;
                case 2: return Play2;
                case 3: return Play3;
                case 4: return Play4;
                case 5: return Play5;
                case 6: return Play6;
                case 7: return Play7;
            }
            return null;
        }

        private TrackBar BarSlider(int SliderNumber)
        {
            switch (SliderNumber)
            {
                case 0: return Bar0;
                case 1: return Bar1;
                case 2: return Bar2;
                case 3: return Bar3;
                case 4: return Bar4;
                case 5: return Bar5;
                case 6: return Bar6;
                case 7: return Bar7;
            }
            return null;
        }

        private Label BarCount(int LabelNumber)
        {
            switch (LabelNumber)
            {
                case 0: return Label0;
                case 1: return Label1;
                case 2: return Label2;
                case 3: return Label3;
                case 4: return Label4;
                case 5: return Label5;
                case 6: return Label6;
                case 7: return Label7;
            }
            return null;
        }

        private Button TransposeUp(int ButtonNumber)
        {
            switch (ButtonNumber)
            {
                case 0: return up0;
                case 1: return up1;
                case 2: return up2;
                case 3: return up3;
                case 4: return up4;
                case 5: return up5;
                case 6: return up6;
                case 7: return up7;
            }
            return null;
        }

        private Button TransposeDown(int ButtonNumber)
        {
            switch (ButtonNumber)
            {
                case 0: return down0;
                case 1: return down1;
                case 2: return down2;
                case 3: return down3;
                case 4: return down4;
                case 5: return down5;
                case 6: return down6;
                case 7: return down7;
            }
            return null;
        }

        private Label NoteLabel(int x, int y)
        {
            switch(Convert.ToString(x) + Convert.ToString(y))
            {
                case "00": return label8;
                case "01": return label9;
                case "02": return label10;
                case "03": return label11;

                case "10": return label12;
                case "11": return label13;
                case "12": return label14;
                case "13": return label15;

                case "20": return label16;
                case "21": return label17;
                case "22": return label18;
                case "23": return label19;

                case "30": return label20;
                case "31": return label21;
                case "32": return label22;
                case "33": return label23;

                case "40": return label24;
                case "41": return label25;
                case "42": return label26;
                case "43": return label27;

                case "50": return label28;
                case "51": return label29;
                case "52": return label30;
                case "53": return label31;

                case "60": return label32;
                case "61": return label33;
                case "62": return label34;
                case "63": return label35;

                case "70": return label36;
                case "71": return label37;
                case "72": return label38;
                case "73": return label39;
            }
            return null;
        }
        
        private Button FavAdd(int Number)
        {
            switch (Number)
            {
                case 0: return Add0;
                case 1: return Add1;
                case 2: return Add2;
                case 3: return Add3;
                case 4: return Add4;
                case 5: return Add5;
                case 6: return Add6;
                case 7: return Add7;
            }
            return null;
        }

        private Button FavRemove(int Number)
        {
            switch (Number)
            {
                case 0: return Remove0;
                case 1: return Remove1;
                case 2: return Remove2;
                case 3: return Remove3;
                case 4: return Remove4;
                case 5: return Remove5;
                case 6: return Remove6;
                case 7: return Remove7;
            }
            return null;
        }

        #endregion

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Bar Length Sliders

        private void Bar0_Scroll(object sender, EventArgs e)
        {
            int BarNumber = 0;
            BarScrollBehaviour(BarNumber);
        }

        private void Bar1_Scroll(object sender, EventArgs e)
        {
            int BarNumber = 1;
            BarScrollBehaviour(BarNumber);
        }

        private void Bar2_Scroll(object sender, EventArgs e)
        {
            int BarNumber = 2;
            BarScrollBehaviour(BarNumber);
        }

        private void Bar3_Scroll(object sender, EventArgs e)
        {
            int BarNumber = 3;
            BarScrollBehaviour(BarNumber);
        }

        private void Bar4_Scroll(object sender, EventArgs e)
        {
            int BarNumber = 4;
            BarScrollBehaviour(BarNumber);
        }

        private void Bar5_Scroll(object sender, EventArgs e)
        {
            int BarNumber = 5;
            BarScrollBehaviour(BarNumber);
        }

        private void Bar6_Scroll(object sender, EventArgs e)
        {
            int BarNumber = 6;
            BarScrollBehaviour(BarNumber);
        }

        private void Bar7_Scroll(object sender, EventArgs e)
        {
            int BarNumber = 7;
            BarScrollBehaviour(BarNumber);
        }

        #endregion

        private void BarScrollBehaviour(int BarNumber)
        {
            UpdateFreeBeats(BarNumber);
            UpdateBarLength();
        }

        private void UpdateBarLength()
        {
            for (int i = 0; i < trackBar1.Value + 1; i++)
            {
                BarCount(i).Text = Convert.ToString(BarSlider(i).Value);
            }
        }

        private void UpdateFreeBeats(int Except)
        {
            FreeBeats = 16;
            for (int i = 0; i < trackBar1.Value + 1; i++)
            {
                FreeBeats = FreeBeats - BarSlider(i).Value;
            }
            FreeSpaceLabel.Text = "Free Space: " + FreeBeats;
            if (FreeBeats == 0 & ChordsFilled())
            {
                Export.Visible = true;
                PlayAll.Visible = true;
            }
            else
            {
                PlayAll.Visible = false;
                Export.Visible = false;
            }
            if (FreeBeats < 0)
            {
                for (int i = 0; i < trackBar1.Value + 1; i++)
                {
                    if (BarSlider(i).Value != 1 & Except != i)
                    {
                        BarSlider(i).Value -= 1;
                        break;
                    }
                }
                UpdateFreeBeats(Except);
            }
        }

        private bool ChordsFilled()
        {
            for (int i = 0; i < trackBar1.Value + 1; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (AllChords[i, j] == -1) return false;
                }
            }
            return true;
        }

        #region Rand Clicks

        private void RandNew7_Click(object sender, EventArgs e)
        {
            RandClick(7);
        }

        private void RandNew6_Click(object sender, EventArgs e)
        {
            RandClick(6);
        }

        private void RandNew5_Click(object sender, EventArgs e)
        {
            RandClick(5);
        }

        private void RandNew4_Click(object sender, EventArgs e)
        {
            RandClick(4);
        }

        private void RandNew3_Click(object sender, EventArgs e)
        {
            RandClick(3);
        }

        private void RandNew2_Click(object sender, EventArgs e)
        {
            RandClick(2);
        }

        private void RandNew1_Click(object sender, EventArgs e)
        {
            RandClick(1);
        }

        private void RandNew0_Click(object sender, EventArgs e)
        {
            RandClick(0);
        }

        private void RandClick(int ChordNumber)
        {
            RandChord(ChordNumber);
            if (AutoPlay.Checked) PlayChord(ChordNumber);
            UpdateFreeBeats(ChordNumber);
        }

        #endregion

        private void RandChord(int ChordNumber)
        {
            for (int i = 0; i < 4; i++)
            {
                AllChords[ChordNumber, i] = -1;
            }
            byte[] Fav = File.ReadAllBytes(Application.StartupPath + "/Library/favourites.txt");
            if (random.Next(0, 2) == 0 || Fav.Length == 4)
            {
                int NewNote = 0;
                for (int i = 0; i < 4; i++)
                {
                    bool NoteTaken = true;
                    while (NoteTaken == true)
                    {
                        NewNote = random.Next(0, 24);
                        NoteTaken = false;
                        for (int j = 0; j < 4; j++)
                        {
                            if (AllChords[ChordNumber, j] == NewNote) NoteTaken = true;
                            if (AllChords[ChordNumber, j] == NewNote + 1) NoteTaken = true;
                            if (AllChords[ChordNumber, j] == NewNote - 1) NoteTaken = true;
                        }
                    }
                    AllChords[ChordNumber, i] = NewNote;
                }
            }
            else
            {
                Console.WriteLine("favourite taken");
                int Pos = random.Next(1, Fav.Length / 4);
                for (int i = 0; i < 4; i++)
                {
                    AllChords[ChordNumber, i] = Fav[Pos * 4 + i];
                }
                int Shift = random.Next(0, 24);
                for (int i = 0; i < 4; i++)
                {
                    if (AllChords[ChordNumber, i] + Shift < 24)
                    {
                        AllChords[ChordNumber, i] += Shift;
                    }
                    else
                    {
                        AllChords[ChordNumber, i] += Shift - 24;
                    }
                }
            }
            SortNotes(ChordNumber);
            UpdateNotes();
            UpdateFavLabels(ChordNumber);
        }

        private void SortNotes(int ChordNumber)
        {
            int[] TempNotes = new int[4];
            for (int i = 0; i < 4; i++)
            {
                TempNotes[i] = AllChords[ChordNumber, i];
            }
            List<int> copy = new List<int>(TempNotes);
            copy.Sort();
            for (int i = 0; i < 4; i++)
            {
                AllChords[ChordNumber, i] = copy[i];
            }
        }

        private void UpdateNotes()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if ((AllChords[i, j] == -1))
                    {
                        NoteLabel(i, j).Text = "";
                    }
                    else
                    {
                        NoteLabel(i, j).Text = Convert.ToString(AllChords[i, j]);
                    }
                }
            }
        }

        private void RandLength_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < trackBar1.Value + 1; i++)
            {
                BarSlider(i).Value = 1;
                BarScrollBehaviour(i);
            }
            while (FreeBeats > 0)
            {
                int ChordNumber = random.Next(0, trackBar1.Value + 1);
                if (BarSlider(ChordNumber).Value < BarSlider(ChordNumber).Maximum)
                {
                    BarSlider(ChordNumber).Value += 1;
                    BarScrollBehaviour(ChordNumber);
                }
            }
        }

        #region Play Clicks

        private void Play7_Click(object sender, EventArgs e)
        {
            PlayChord(7);
        }

        private void Play6_Click(object sender, EventArgs e)
        {
            PlayChord(6);
        }

        private void Play5_Click(object sender, EventArgs e)
        {
            PlayChord(5);
        }

        private void Play4_Click(object sender, EventArgs e)
        {
            PlayChord(4);
        }

        private void Play3_Click(object sender, EventArgs e)
        {
            PlayChord(3);
        }

        private void Play2_Click(object sender, EventArgs e)
        {
            PlayChord(2);
        }

        private void Play1_Click(object sender, EventArgs e)
        {
            PlayChord(1);
        }

        private void Play0_Click(object sender, EventArgs e)
        {
            PlayChord(0);
        }

        #endregion

        private void PlayChord(int ChordNumber)
        {
            const int HeadSize = 44;
            byte[] Main = File.ReadAllBytes(Library + "0.wav");
            for (int i = HeadSize; i < Main.Length; i++)
            {
                Main[i] = 0;
            }
            for (int i = 0; i < 4; i++)
            {
                if (AllChords[ChordNumber, i] == -1) return;
                Add(Main, File.ReadAllBytes(Library + AllChords[ChordNumber, i] + ".wav"), 0,false);
            }
            string FilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".wav");
            System.IO.File.WriteAllBytes(FilePath, Main);
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(FilePath);
            player.Play();
        }
        
        private void Add(byte[] TargetWav, byte[] Addend, int Delay, bool Replace)
        {
            const int Max = 32767;
            const int Min = 32768;
            Delay = Convert.ToInt32(Delay / 4) * 4;
            if (Replace)
            {
                for (int i = 44; i < Addend.Length; i++)
                {
                    if (i + Delay >= TargetWav.Length) break;
                    TargetWav[i + Delay] = Addend[i];
                }
            }
            else
            {
                for (int i = 44; i < Addend.Length - 200; i += 2)
                {
                    if (i + Delay < TargetWav.Length - 1)
                    {
                        int TargetValue = Read(TargetWav, i + Delay);
                        int AddendValue = Read(Addend, i);

                        if (TargetValue + AddendValue > 65535)
                        {
                            if (TargetValue >= Min & AddendValue >= Min & TargetValue + AddendValue - 65536 < Min)
                            {
                                Write(TargetWav, Min, i + Delay);
                            }
                            else
                            {
                                Write(TargetWav, TargetValue + AddendValue - 65536, i + Delay);
                            }
                        }
                        else
                        {
                            if (TargetValue <= Max & AddendValue <= Max & TargetValue + AddendValue > Max)
                            {
                                Write(TargetWav, Max, i + Delay);
                            }
                            else
                            {
                                Write(TargetWav, TargetValue + AddendValue, i + Delay);
                            }
                        }
                    }
                }
            }
        }

        public int Read(byte[] TargetWav, int Position)
        {
            return TargetWav[Position + 1] * 256 + TargetWav[Position];
        }

        private void Write(byte[] TargetWav, int Value, int Position)
        {
            TargetWav[Position] = Convert.ToByte(Value % 256);
            TargetWav[Position + 1] = Convert.ToByte(Math.Floor(Convert.ToDecimal(Value / 256)));
        }

        #region Transpose Clicks

        //0
        private void button1_Click(object sender, EventArgs e)
        {
            Transpose(true, 0);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Transpose(false, 0);
        }

        //1
        private void button2_Click(object sender, EventArgs e)
        {
            Transpose(true, 1);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Transpose(false, 1);
        }

        //2
        private void button3_Click(object sender, EventArgs e)
        {
            Transpose(true, 2);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Transpose(false, 2);
        }

        //3
        private void button4_Click(object sender, EventArgs e)
        {
            Transpose(true, 3);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Transpose(false, 3);
        }

        //4
        private void button8_Click(object sender, EventArgs e)
        {
            Transpose(true, 4);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Transpose(false, 4);
        }

        //5
        private void button7_Click(object sender, EventArgs e)
        {
            Transpose(true, 5);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Transpose(false, 5);
        }

        //6
        private void button6_Click(object sender, EventArgs e)
        {
            Transpose(true, 6);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Transpose(false, 6);
        }

        //7
        private void button5_Click(object sender, EventArgs e)
        {
            Transpose(true, 7);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Transpose(false, 7);
        }

        #endregion

        private void Transpose(bool Up, int ChordNumber)
        {
            for (int i = 0; i < 4; i++)
            {
                if (AllChords[ChordNumber, i] == -1) return;
            }
            if (Up)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (AllChords[ChordNumber, i] == 23)
                    {
                        AllChords[ChordNumber, i] = 0;
                    }
                    else
                    {
                        AllChords[ChordNumber, i] = AllChords[ChordNumber, i] + 1;
                    }
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    if (AllChords[ChordNumber, i] == 0)
                    {
                        AllChords[ChordNumber, i] = 23;
                    }
                    else
                    {
                        AllChords[ChordNumber, i] = AllChords[ChordNumber, i] - 1;
                    }
                }
            }
            SortNotes(ChordNumber);
            UpdateNotes();
            if (AutoPlay.Checked) PlayChord(ChordNumber);
        }

        private void Export_Click(object sender, EventArgs e)
        {
            byte[] ReferenceHeader = File.ReadAllBytes(Application.StartupPath + "/Library/reference.mid");
            byte[] Midi = new byte[64 + (trackBar1.Value + 1) * 32];
            for (int i = 0; i < 61; i++)
            {
                Midi[i] = ReferenceHeader[i];
            }
            Midi[13] = 2;
            if ((trackBar1.Value + 1) * 32 + 15 < 256)
            {
                Midi[48] = Convert.ToByte((trackBar1.Value + 1) * 32 + 15);
            }
            else
            {
                Midi[47] = 1;
                Midi[48] = Convert.ToByte((trackBar1.Value + 1) * 32 + 15 - 256);
            }
            for (int i = 0; i < trackBar1.Value + 1; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int Delay = 61 + (i * 32) + (j * 4);
                    Midi[Delay] = 144;
                    Midi[Delay + 1] = Convert.ToByte(48 + AllChords[i, j]);
                    Midi[Delay + 2] = 127;
                    Midi[Delay + 3] = 0;
                }
                Midi[61 + (i * 32) + 15] = Convert.ToByte(BarSlider(i).Value);
                for (int j = 0; j < 4; j++)
                {
                    int Delay = 61 + (i * 32) + (j * 4);
                    Midi[Delay + 16] = 128;
                    Midi[Delay + 17] = Convert.ToByte(48 + AllChords[i, j]);
                    Midi[Delay + 18] = 127;
                    Midi[Delay + 19] = 0;
                }
            }
            Midi[61 + (trackBar1.Value + 1) * 32] = 255;
            Midi[61 + (trackBar1.Value + 1) * 32 + 1] = 47;
            Midi[61 + (trackBar1.Value + 1) * 32 + 2] = 0;
            SaveFileDialog SaveMelody = new SaveFileDialog();
            
            SaveMelody.FileName = "Midi Chords.mid";
            
            SaveMelody.Filter = "MIDI Files (*.mid)|*.mid";
            if (SaveMelody.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(SaveMelody.FileName, Midi);
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            Bpm.Text = "BPM: " + trackBar2.Value;
        }

        private void PlayAll_Click(object sender, EventArgs e)
        {
            if (ChordsFilled() == false) return;
            const int SamplesPerMinute = 44100 * 60;
            int SamplesPerBeat = 4 * SamplesPerMinute / trackBar2.Value;
            byte[] Main = new byte[44 + SamplesPerBeat * 20];
            byte[] Header = File.ReadAllBytes(Application.StartupPath + "/Library/0.wav");
            for (int i = 0; i < 44; i++)
            {
                Main[i] = Header[i];
            }
            AdjHeadLengthInfo(Main, 44);
            AdjHeadLengthInfo(Main, 8);
            byte[] Temp = new byte[44 + SamplesPerBeat * 12];
            int Delay = 0;
            for (int i = 0; i < trackBar1.Value + 1; i++)
            {
                Add(Temp, ChordPreview(i), Delay * SamplesPerBeat / 2, true);
                Delay += BarSlider(i).Value;
            }
            Add(Main, Temp, 0,true);
            Add(Main, Temp, SamplesPerBeat * 8, true);
            for (int i = 0; i < 16; i++)
            {
                if (i % 4 != 0) Add(Main, File.ReadAllBytes(Library + "click.wav"), i * SamplesPerBeat, false);
                else Add(Main, File.ReadAllBytes(Library + "clickhigh.wav"), i * SamplesPerBeat, false);
            }
            string FilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".wav");
            System.IO.File.WriteAllBytes(FilePath, Main);
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(FilePath);
            player.Play();
        }

        private byte[] ChordPreview(int ChordNumber)
        {
            byte[] Main = File.ReadAllBytes(Library + "0.wav");
            for (int i = 44; i < Main.Length; i++)
            {
                Main[i] = 0;
            }
            for (int i = 0; i < 4; i++)
            {
                Add(Main, File.ReadAllBytes(Library + AllChords[ChordNumber, i] + ".wav"), 0, false);
            }
            return Main;
        }

        private void AdjHeadLengthInfo(byte[] TargetHeader, int StartPos)
        {
            for (int i = 0; i < 4; i++)
            {
                int Length = TargetHeader.Length - StartPos;
                byte Byte = (byte)(Math.Floor(Length / Math.Pow(256, i) % 256));
                
                TargetHeader[StartPos - 4 + i] = Byte;
            }
        }

        #region Add / Remove Clicks

        private void Add0_Click(object sender, EventArgs e)
        {
            AddNotes(0);
        }

        private void Remove0_Click(object sender, EventArgs e)
        {
            RemoveNotes(0);
        }

        private void Add1_Click(object sender, EventArgs e)
        {
            AddNotes(1);
        }

        private void Remove1_Click(object sender, EventArgs e)
        {
            RemoveNotes(1);
        }

        private void Add2_Click(object sender, EventArgs e)
        {
            AddNotes(2);
        }

        private void Remove2_Click(object sender, EventArgs e)
        {
            RemoveNotes(2);
        }

        private void Add3_Click(object sender, EventArgs e)
        {
            AddNotes(3);
        }

        private void Remove3_Click(object sender, EventArgs e)
        {
            RemoveNotes(3);
        }

        private void Add4_Click(object sender, EventArgs e)
        {
            AddNotes(4);
        }

        private void Remove4_Click(object sender, EventArgs e)
        {
            RemoveNotes(4);
        }

        private void Add5_Click(object sender, EventArgs e)
        {
            AddNotes(5);
        }

        private void Remove5_Click(object sender, EventArgs e)
        {
            RemoveNotes(5);
        }

        private void Add6_Click(object sender, EventArgs e)
        {
            AddNotes(6);
        }

        private void Remove6_Click(object sender, EventArgs e)
        {
            RemoveNotes(6);
        }

        private void Add7_Click(object sender, EventArgs e)
        {
            AddNotes(7);
        }

        private void Remove7_Click(object sender, EventArgs e)
        {
            RemoveNotes(7);
        }

        #endregion

        private void AddNotes(int ChordNumber)
        {
            for (int i = 0; i < 4; i++)
            {
                if (AllChords[ChordNumber, i] == -1) return;
            }
            byte[] Favourite = File.ReadAllBytes(Application.StartupPath + "/Library/favourites.txt");
            byte[] Insert = new byte[Favourite.Length + 4];
            for (int i = 0; i < Favourite.Length; i++)
            {
                Insert[i] = Favourite[i];
            }
            for (int i = 0; i < 4; i++)
            {
                Insert[Favourite.Length + i] = (byte)AllChords[ChordNumber, i];
            }
            File.WriteAllBytes(Application.StartupPath + "/Library/favourites.txt", Insert);
            UpdateFavLabels(ChordNumber);
        }

        private void RemoveNotes(int ChordNumber)
        {
            int Pos = FindNote(ChordNumber);
            if (Pos == -3) return;
            byte[] Fav = File.ReadAllBytes(Application.StartupPath + "/Library/favourites.txt");
            byte[] Insert = new byte[Fav.Length - 4];
            for (int i = 0; i < Pos; i++)
            {
                Insert[i] = Fav[i];
            }
            for (int i = Pos + 4; i < Fav.Length; i++)
            {
                Insert[i - 4] = Fav[i];
            }
            File.WriteAllBytes(Application.StartupPath + "/Library/favourites.txt", Insert);
            UpdateFavLabels(ChordNumber);
        }

        private int FindNote(int ChordNumber)
        {
            byte[] Fav = File.ReadAllBytes(Application.StartupPath + "/Library/favourites.txt");
            int[] Chord = new int[4];
            for (int i = 0; i < 4; i++)
            {
                Chord[i] = AllChords[ChordNumber, i];
            }
            int MatchCount = 0;
            int MatchPos = 0;
            for (int j = 0; j < 24; j++)
            {
                for (int i = 0; i < Fav.Length; i++)
                {
                    if (Fav[i] == Chord[i % 4])
                    {
                        MatchCount++;
                    }
                    else
                    {
                        MatchCount = 0;
                    }
                    if (MatchCount == 4)
                    {
                        MatchPos = i;
                        break;
                    }
                }
                if (MatchCount == 4) break;
                for (int i = 0; i < 4; i++)
                {
                    if (Chord[i] + 1 < 24)
                    {
                        Chord[i] = Chord[i] + 1;
                    }
                    else
                    {
                        Chord[i] = 0;
                    }
                }
                List<int> copy = new List<int>(Chord);
                copy.Sort();
                for (int i = 0; i < 4; i++)
                {
                    Chord[i] = copy[i];
                }
            }
            return MatchPos - 3;
        }

        private void UpdateFavLabels(int ChordNumber)
        {
            if (FindNote(ChordNumber) == -3)
            {
                FavAdd(ChordNumber).Visible = true;
                FavRemove(ChordNumber).Visible = false;
            }
            else
            {
                FavAdd(ChordNumber).Visible = false;
                FavRemove(ChordNumber).Visible = true;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(".");
            player.Stop();
        }

        #region Label Clicks

        //x = 0
        private void label11_Click(object sender, EventArgs e)
        {
            LabelClick(0, 3);
        }

        private void label10_Click(object sender, EventArgs e)
        {
            LabelClick(0, 2);
        }

        private void label9_Click(object sender, EventArgs e)
        {
            LabelClick(0, 1);
        }

        private void label8_Click(object sender, EventArgs e)
        {
            LabelClick(0, 0);
        }

        //x = 1
        private void label15_Click(object sender, EventArgs e)
        {
            LabelClick(1, 3);
        }

        private void label14_Click(object sender, EventArgs e)
        {
            LabelClick(1, 2);
        }

        private void label13_Click(object sender, EventArgs e)
        {
            LabelClick(1, 1);
        }

        private void label12_Click(object sender, EventArgs e)
        {
            LabelClick(1, 0);
        }

        //x = 2
        private void label19_Click(object sender, EventArgs e)
        {
            LabelClick(2, 3);
        }

        private void label18_Click(object sender, EventArgs e)
        {
            LabelClick(2, 2);
        }

        private void label17_Click(object sender, EventArgs e)
        {
            LabelClick(2, 1);
        }

        private void label16_Click(object sender, EventArgs e)
        {
            LabelClick(2, 0);
        }

        //x = 3
        private void label23_Click(object sender, EventArgs e)
        {
            LabelClick(3, 3);
        }

        private void label22_Click(object sender, EventArgs e)
        {
            LabelClick(3, 2);
        }

        private void label21_Click(object sender, EventArgs e)
        {
            LabelClick(3, 1);
        }

        private void label20_Click(object sender, EventArgs e)
        {
            LabelClick(3, 0);
        }

        //x = 4
        private void label27_Click(object sender, EventArgs e)
        {
            LabelClick(4, 3);
        }

        private void label26_Click(object sender, EventArgs e)
        {
            LabelClick(4, 2);
        }

        private void label25_Click(object sender, EventArgs e)
        {
            LabelClick(4, 1);
        }

        private void label24_Click(object sender, EventArgs e)
        {
            LabelClick(4, 0);
        }

        //x = 5
        private void label31_Click(object sender, EventArgs e)
        {
            LabelClick(5, 3);
        }

        private void label30_Click(object sender, EventArgs e)
        {
            LabelClick(5, 2);
        }

        private void label29_Click(object sender, EventArgs e)
        {
            LabelClick(5, 1);
        }

        private void label28_Click(object sender, EventArgs e)
        {
            LabelClick(5, 0);
        }

        //x = 6
        private void label35_Click(object sender, EventArgs e)
        {
            LabelClick(6, 3);
        }

        private void label34_Click(object sender, EventArgs e)
        {
            LabelClick(6, 2);
        }

        private void label33_Click(object sender, EventArgs e)
        {
            LabelClick(6, 1);
        }

        private void label32_Click(object sender, EventArgs e)
        {
            LabelClick(6, 0);
        }

        //x = 7
        private void label39_Click(object sender, EventArgs e)
        {
            LabelClick(7, 3);
        }

        private void label38_Click(object sender, EventArgs e)
        {
            LabelClick(7, 2);
        }

        private void label37_Click(object sender, EventArgs e)
        {
            LabelClick(7, 1);
        }

        private void label36_Click(object sender, EventArgs e)
        {
            LabelClick(7, 0);
        }

        #endregion

        private void LabelClick(int x, int y)
        {
            int i = 0;
            bool Valid = false;
            while (Valid == false)
            {
                string Input = Interaction.InputBox("Input Note", "Manual Input", NoteLabel(x, y).Text);
                if (Input == "") return;
                if (int.TryParse(Input, out i) & i >= 0 & i < 24)
                {
                    Valid = true;
                    break;
                }
                MessageBox.Show("Value invalid, please try again.");
            }
            if (Valid) AllChords[x, y] = i;
            SortNotes(x);
            UpdateNotes();
            UpdateFavLabels(x);
            if (AutoPlay.Checked) PlayChord(x);
        }
    }
}
