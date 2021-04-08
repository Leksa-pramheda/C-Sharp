using System;
using System.IO;
//using System.Text;
//using Microsoft.Win32.SafeHandles;
//using System.Threading.Tasks;

namespace нейраааа
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 2;
            string[] trs_in = new string[x]; // массив неповторяющихся ссылок на видео
            int r = 0;//вспомогательная переменная для определения необходимости увеличения размера массива
            int kol = 0;//целочисленная переменная, указывающая на количество строк в файлах
            int r1 = 0;//вспомогательная переменная для определения необходимости увеличения размера массива
            int t = -1;//вспомогательная переменная для нахождения количества строк в файлах
            FileStream fin;
            FileStream fint;
            FileStream fins2;

            FileStream fine;
            FileStream fint2;
            int j = 0;//целочисленная переменная, используемая в циклах
            int i = 0;//целочисленная переменная, используемая в циклах
            string line;//строковая переменная для чтения данных
            string c;//вспомогательная переменная для чтения данных из файла и их последующей записи
            string[] a = new string[3]; //вспомогательный массив для построчного чтения
            fine = new FileStream("D:\\neuro\\данные\\большие данные тренинг вход.txt", FileMode.Open);//открытие файла входных данных для тренировочной выборки
            using (StreamReader sr = new StreamReader(fine, System.Text.Encoding.Default)) //открываем пространство файлов fine для чтения
            {//считываем данные тренировочной выборки
                line = sr.ReadLine();//считываем первую строку
                for (i = 0; i < line.Length; i++) //вычленяем данные выборки и распределяем их в массиве так,
                {
                    c = Convert.ToString(line[i]);//чтобы нулевым элементом массива оказалась ссылка на первое видео этой строки выборки
                    if (c == ",") //первым элементом-ссылка на второе видео, третьим-выбор пользователя (левое или правое)
                        j = j + 1;
                    else
                        a[j] = a[j] + line[i];


                }//Далее названия адреса ссылок записываются в массив trs_in 
                trs_in[0] = a[0];
                trs_in[1] = a[1];
                while ((line = sr.ReadLine()) != null)//Операция повторяется, пока программа не достигнет конца файла входных данных
                {
                    j = 0;
                    t = t + 1;
                    a[0] = null;
                    a[1] = null;
                    a[2] = null;
                    for (i = 0; i < line.Length; i++)
                    {
                        c = Convert.ToString(line[i]);
                        if (c == ",")
                            j = j + 1;
                        else
                            a[j] = a[j] + line[i];


                    }

                    int r2 = 0;//вспомогательная переменная для определения необходимости увеличения размера массива
                    for (int u = 0; u < trs_in.Length; u++)//но перед записью адреса ссылки в массив trs_in программа проверяет
                    {// записывалась ли эта ссылка прежде
                        //Для этого
                        if (a[0] != trs_in[u])//вычисляется количество ссылок из предыдущих строк, не идентичных левой ссылке текущей строки
                        {
                            r2 = r2 + 1;
                        }
                        if (a[1] != trs_in[u])//вычисляется количество ссылок из предыдущих строк, не идентичных правой ссылке текущей строки
                        {
                            r1 = r1 + 1;
                        }
                    }
                    
                    if (r2 == trs_in.Length)//и сравниваются вычисленное количество с текущим количеством элементов в массиве
                    {//если они равны, значит эта ссылка не записывалась прежде
                        Array.Resize<string>(ref trs_in, trs_in.Length + 1);//поэтому размер массива trs_in увеличивается на 1 элемент
                        trs_in[trs_in.Length - 1] = a[0];//и в появившуюся позицию записывается новая ссылка
                        r = -1;
                    }
                    if (((r1 == trs_in.Length) && (r == 0)) || ((r1 == trs_in.Length - 1) && (r == -1)))//происходит аналогичная проверка
                    {
                        Array.Resize<string>(ref trs_in, trs_in.Length + 1);
                        trs_in[trs_in.Length - 1] = a[1];
                        r = 0;
                    }
                    r = 0;//счетчики обнуляются
                    r2 = 0;
                    r1 = 0;
                }
            }
            fine.Close();
            kol = t + 2;
            //открытие вспомогательных файлов для работы с большим количеством данных (изначально файлы пустые)
            var path = "D:\\neuro\\данные\\5.txt"; //файл для записи и чтения массива тренировочной выборки, в котором обозначаются ссылки, которые участвуют в сравнении в конкретной строке 
            var path0 = "D:\\neuro\\данные\\7 test.txt";//файл для записи и чтения массива тестовой выборки, в котором обозначаются ссылки, которые участвуют в сравнении в конкретной строке 
            var path1 = "D:\\neuro\\данные\\8 test.txt";//файл для записи и чтения массива тестовой выборки предпочтений пользователя: левый =1, правый=2
            var path2 = "D:\\neuro\\данные\\6.txt";//файл для записи и чтения массива тренировочной выборки предпочтений пользователя: левый =1, правый=2

            double[] tr_out = new double[kol];//массив, в который будут записаны правильные выходные данные из выборки
            double[] random = new double[trs_in.Length];//массив весов выборки
            fine = new FileStream("D:\\neuro\\данные\\большие данные тренинг вход.txt", FileMode.Open); 
            using (StreamReader sr = new StreamReader(fine, System.Text.Encoding.Default))//повторное открытие и считывание входных данных тренировочной выборки 
            {
                t = -1;

                using (StreamWriter stw = File.AppendText(path2)) //открытие файла для записи
                {

                    using (StreamWriter sff = File.AppendText(path))//открытие файла для записи
                    {
                        while ((line = sr.ReadLine()) != null)//построчное чтение файла, пока в нем не закончатся данные 
                        {
                            j = 0;
                            t = t + 1;
                            a[0] = null;
                            a[1] = null;
                            a[2] = null;

                            for (i = 0; i < line.Length; i++)//распределение входных данных по третем массивам
                            {
                                c = Convert.ToString(line[i]);
                                if (c == ",")
                                    j = j + 1;
                                else
                                    a[j] = a[j] + line[i];
                            }
                            for (int u = 0; u < trs_in.Length; u++)
                            {
                                if ((a[0] == trs_in[u]) || (a[1] == trs_in[u]))//поиск ссылки из текущей строки в массиве неповторяющихся ссылок; в случае совпадения ссылок
                                    sff.Write("1");//в файле отмечается наличие=1
                                else//в случае отсутствия
                                    sff.Write("0");//отмчается 0
                                if (a[0] == trs_in[u])//отметка о стороне ссылки в конкретной строке (левый =1, правый=2)
                                    stw.Write("1");
                                else
                                if (a[1] == trs_in[u])
                                    stw.Write("2");
                                else//если ссылка не совпала, записывается 0 
                                    stw.Write("0");
                                if ((a[2] == "left"))//запись "правильных ответов" тренировочной выборки (левый =1, правый=2) в массив
                                {
                                    tr_out[t] = 1;
                                }
                                else
                                if (a[2] == "right")
                                {
                                    tr_out[t] = 2;
                                }

                            }
                            sff.WriteLine();
                            stw.WriteLine();
                        }
                    }
                }
                string b = "D:\\neuro\\данные\\большие данные тренинг выход.docx";//пустой файл, куда будут записываться найденные ответы
                
                Random rnd = new Random();

                double[] outs = new double[kol];//вспомогательный массив для регулировки весов
                double[] err = new double[kol];//вспомогательный массив для нахождения разности между "правильным ответом" и найденным на конкретном числе итераций
                double[] errs = new double[kol];//вспомогательный массив для нахождения степени ошибки
                using (StreamWriter sw = new StreamWriter(b, true, System.Text.Encoding.Default))//открытие файла для записи
                {

                    for (i = 0; i < trs_in.Length; i++)//установка случайных весов на данном этапе
                    {
                        for (j = 0; j < 1; j++)
                        {
                            random[i] = 2 * (float)rnd.Next(-1, 1) / 10f;
                        }
                    }
                    sw.WriteLine();
                    File.Copy(path, "D:\\neuro\\данные\\5 копи.txt", true);//копирование записанного файла вместе с его данными, для последующей возможности считывания
                    fin = new FileStream("D:\\neuro\\данные\\5 копи.txt", FileMode.Open);
                    using (StreamReader sfr = new StreamReader(fin,  System.Text.Encoding.Default))//открытие файла на чтение
                    {
                    
                        int kar = 0;

                        for (int k2 = 0; k2 < 4000; k2++)//выравнивание весов на 4000 итераций
                        {
                            t = t - 1;
                            int p = 1;
                            fin.Seek(0, SeekOrigin.Begin);//перевод курсора на начало файла
                            for (i = 0; i < kol; ++i)
                            {
                                string content3 = sfr.ReadLine();//построчное чтение файла, в котором обозначаются ссылки, которые участвуют в сравнении в конкретной строке 
                                for (j = 0; j < trs_in.Length; ++j)
                                    for ( int k = 0; k < p; ++k)
                                    {
                                         kar = content3[j];
                                        if (kar == 49)//ручной перевод чисел из Unicode в ASCII
                                            kar = 1;
                                        else kar = 0;
                                        outs[i] += kar * random[j];//перемножение матриц для регулировки весов в зависимости от наличия или отсутствия ссылки в строках
                                    }
                            }

                            for (i = 0; i < kol; i++)
                            {
                                    outs[i] = (1 / (1 + Math.Exp(-outs[i]))); //нахождение сигмоиды для каждого элемента массива
                            }
                            for (i = 0; i < tr_out.GetLength(0); ++i)
                            {
                                for (j = 0; j < 1; ++j)
                                {
                                    err[i] = tr_out[i] - outs[i];//нахождение разности между "правильным ответом" и найденным на текущем этапе
                                    errs[i] = err[i] * (outs[i] * (1 - outs[i]));//нахождение степени ошибки
                                }
                            }

                            double[,] adj = new double[trs_in.Length, 1];//массив для корректировки весов
                            p = 1;

                            fin.Seek(0, SeekOrigin.Begin);//перевод курсора на начало файла
                            double[] m = new double[trs_in.Length];//вспомогательный массив для корректировки весов
                            for (i = 0; i < kol; ++i)
                            {
                                string content3 = sfr.ReadLine();//построчное чтение файла, в котором обозначаются ссылки, которые участвуют в сравнении в конкретной строке 
                                for (j = 0; j < trs_in.Length; ++j)
                                    for (int k = 0; k < p; ++k)
                                    {
                                        kar = content3[j];//ручной перевод чисел из Unicode в ASCII
                                        if (kar == 49)
                                            kar = 1;
                                        else kar = 0;
                                        m[j] += kar * errs[i];//перемножение матриц для регулировки весов в зависимости от наличия или отсутствия ссылки в строках
                                    }
                            }
                            for (i = 0; i <trs_in.Length; i++)
                            {
                                for (j = 0; j < 1; j++)
                                {
                                    adj[i, j] = m[i];
                                }
                            }
                            for (i = 0; i < trs_in.Length; ++i)
                            {
                                for (j = 0; j < 1; ++j)
                                {
                                    random[i] += adj[i, j];//изменение весов
                                }
                            }

                        }

                        string[,] tr_outs = new string[kol, 1];
                        double[,] a2 = new double[kol, 2];//вспомогательный массив измененных весов
                        double[,] b2 = new double[kol, 2];//вспомогательный массив, содержащий расположение ссылки (слево=1 или справо=2)
                        fin.Seek(0, SeekOrigin.Begin);//перевод курсора на начало файла
                        fint = new FileStream(path2, FileMode.Open);
                        using (StreamReader str = new StreamReader(fint, System.Text.Encoding.Default))//открытие файла на чтение
                        {
                            for (i = 0; i < kol; i++)
                            {
                                int k = -1;
                                string content3 = sfr.ReadLine();//построчное чтение файла, в котором обозначаются ссылки, которые участвуют в сравнении в конкретной строке 
                                string content2 = str.ReadLine();//построчное чтения файла, в котором отмечен порядок расположения ссылок
                                for (j = 0; j < trs_in.Length; j++)
                                {

                                    if (content3[j] == '1')
                                    {
                                        k = k + 1;
                                        kar = content2[j];//ручной перевод чисел из Unicode в ASCII
                                        if (kar == 49)
                                            kar = 1;
                                        else kar = 0;
                                        b2[i, k] = kar;//запись в массив расположение ссылки (слево=1 или справо=2)
                                        a2[i, k] = random[j];//запись в массив измененных весов
                                    }
                                }
                            }
                            for (i = 0; i < kol; i++)
                            {
                                if (a2[i, 0] < a2[i, 1])//сравнение значений весов для конкретной строки
                                {
                                    if ((b2[i, 0] == 1) || (b2[i, 1] == 2))//проверка порядка следования ссылок( сперва та, что слево (=1), потом та, что справо (=2)
                                    {
                                        tr_outs[i, 0] = "right";//тогда запись в конечный массив ответа
                                    }
                                    else //или наоборот
                                    if ((b2[i, 1] == 1) || (b2[i, 0] == 2))
                                    {
                                        tr_outs[i, 0] = "left";
                                    }
                                }
                                else //аналогичные действия в случае, если второе значение веса ссылки не превосходит первое
                                 if (a2[i, 0] >= a2[i, 1])
                                {
                                    if ((b2[i, 0] == 1) || (b2[i, 1] == 2))
                                    {
                                        tr_outs[i, 0] = "left";
                                    }
                                    else
                                    if ((b2[i, 1] == 1) || (b2[i, 0] == 2))
                                    {
                                        tr_outs[i, 0] = "right";
                                    }
                                }
                                else //в случае ошибки вывод соответствующих значений
                                {
                                    tr_outs[i, 0] = "error ! (" + a2[i, 0] + "( " + b2[i, 0] + ") , " + a2[i, 1] + " ( " + b2[i, 1] + ")";
                                }
                            }
                            for (i = 0; i < kol; ++i)
                            {
                                for (j = 0; j < 1; ++j)
                                {
                                    sw.Write(tr_outs[i, j] + " ");//запись массива ответов в файл ответов тренировочной выборки
                                }
                                sw.WriteLine();
                            }
                            sw.WriteLine();

                        }
                    }
                }
            }
                fint.Close();
                FileStream fins;
                                r = 0;
                                r1 = 0;
                                t = -1;
                                double[] random2 = new double[trs_in.Length];//создание нового массива весов для тестовой выборки
                                for (i = 0; i < trs_in.Length; ++i)
                                {
                                    for (j = 0; j < 1; ++j)
                                    {
                                        random2[i] = random[i];//запись текущих значений весов из тренировочной выборки в тестовую
                                    }
                                }
                                fins = new FileStream("D:\\neuro\\данные\\большие данные тест вход.txt", FileMode.Open);//открытие входных данных тестовой выборки
                                using (StreamReader srt = new StreamReader(fins, System.Text.Encoding.Default))//открытие пространства файлов fins для чтения
                                {

                                    string[] at = new string[3];
                                    i = 0;
                                    j = 0;
                                    while ((line = srt.ReadLine()) != null)//Пока программа не достигнет конца файла входных данных
                                    {
                                        j = 0;
                                        t = t + 1;
                                        at[0] = null;
                                        at[1] = null;
                                        at[2] = null;
                                        for (i = 0; i < line.Length; i++)//вычленяем данные выборки и распределяем их в массиве так,
                                        {
                                            c = Convert.ToString(line[i]);//чтобы нулевым элементом массива оказалась ссылка на первое видео этой строки выборки
                                            if (c == ",") //первым элементом-ссылка на второе видео, третьим-выбор пользователя (левое или правое)
                                                j = j + 1;
                                            else
                                                at[j] = at[j] + line[i];


                                        }
                                        int r2 = 0;//вспомогательная переменная для определения необходимости увеличения размера массива
                                        for (int u = 0; u < trs_in.Length; u++)//но перед записью адреса ссылки в массив trs_in программа проверяет
                                        {// записывалась ли эта ссылка прежде
                                         //Для этого
                                            if (at[0] != trs_in[u])//вычисляется количество ссылок из предыдущих строк, не идентичных левой ссылке текущей строки
                                            {
                                                r2 = r2 + 1;
                                            }
                                            if (at[1] != trs_in[u])//вычисляется количество ссылок из предыдущих строк, не идентичных правой ссылке текущей строки
                                            {
                                                r1 = r1 + 1;
                                            }
                                        }

                                        if (r2 == trs_in.Length)//и сравниваются вычисленное количество с текущим количеством элементов в массиве
                                        {//если они равны, значит эта ссылка не записывалась прежде
                                            Array.Resize<string>(ref trs_in, trs_in.Length + 1);//поэтому размер массива trs_in увеличивается на 1 элемент
                                            trs_in[trs_in.Length - 1] = a[0]; //и в появившуюся позицию записывается новая ссылка
                                            r = -1;
                                            Array.Resize<double>(ref random2, random2.Length + 1);

                                        }
                                        if (((r1 == trs_in.Length) && (r == 0)) || ((r1 == trs_in.Length - 1) && (r == -1)))//происходит аналогичная проверка
                                        {
                                            Array.Resize<string>(ref trs_in, trs_in.Length + 1);
                                            trs_in[trs_in.Length - 1] = at[1];
                                            r = 0;
                                            Array.Resize<double>(ref random2, random2.Length + 1);
                                        }
                                        r = 0;//счетчики обнуляются
                                        r2 = 0;
                                        r1 = 0;
                                    }
                                }
                                fins.Close();
                                kol = t + 1;
                                fins = new FileStream("D:\\neuro\\данные\\большие данные тест вход.txt", FileMode.Open);
               using (StreamReader srt = new StreamReader(fins, System.Text.Encoding.Default))//повторное открытие и считывание входных данных тестовой выборки 
               {
                t = -1;
                using (StreamWriter stw2 = File.AppendText(path1))//открытие файла для записи
                {

                    using (StreamWriter sff2 = File.AppendText(path0))//открытие файла для записи
                    {

                        while ((line = srt.ReadLine()) != null)//построчное чтение файла, пока в нем не закончатся данные 
                        {
                            j = 0;
                            t = t + 1;
                            a[0] = null;
                            a[1] = null;
                            a[2] = null;
                            for (i = 0; i < line.Length; i++) //распределение входных данных по трем массивам
                            {
                                c = Convert.ToString(line[i]);
                                if (c == ",")
                                    j = j + 1;
                                else
                                    a[j] = a[j] + line[i];
                            }
                            for (int u = 0; u < trs_in.Length; u++)
                            {

                                if ((a[0] == trs_in[u]) || (a[1] == trs_in[u]))//поиск ссылки из текущей строки в массиве неповторяющихся ссылок
                                {// в случае совпадения ссылок
                                    sff2.Write("1"); //в файле отмечается наличие=1
                                }
                                else//в случае отсутствия
                                { 
                                    sff2.Write("0"); //отмчается 0
                                }
                                if (a[0] == trs_in[u]) //отметка о стороне ссылки в конкретной строке (левый =1, правый=2)
                                { 
                                    stw2.Write("1");
                                }
                                else
                                if (a[1] == trs_in[u])
                                { 
                                    stw2.Write("2");
                                }
                                else //если ссылка не совпала, записывается 0 
                                {
                                    stw2.Write("0");
                                }

                            }
                            stw2.WriteLine();
                            sff2.WriteLine();
                        }
                    }
                }
                string d = "D:\\neuro\\данные\\большие данные тест выход.docx"; //пустой файл, куда будут записываться найденные ответы
                using (StreamWriter swt = new StreamWriter(d, true, System.Text.Encoding.Default))//открытие файла для записи
                {
                    File.Copy(path0, "D:\\neuro\\данные\\7 copy.txt", true); //копирование записанного файла вместе с его данными, для последующей возможности считывания
                    fins2 = new FileStream("D:\\neuro\\данные\\7 copy.txt", FileMode.Open);
                    using (StreamReader sfr2 = new StreamReader(fins2, System.Text.Encoding.Default))//открытие файла на чтение
                    {
                        string[,] test_outs = new string[kol, 1];//массив выходных данных тестовой выборки
                        double[,] a22 = new double[kol, 2]; //вспомогательный массив измененных весов
                        double[,] b22 = new double[kol, 2];//вспомогательный массив, содержащий расположение ссылки (слево=1 или справо=2)            
                        fins2.Seek(0, SeekOrigin.Begin);//перевод курсора на начало файла
                        fint2 = new FileStream(path1, FileMode.Open);
                        using (StreamReader str2 = new StreamReader(fint2, System.Text.Encoding.Default))//открытие файла на чтение
                        {
                             for (i = 0; i < kol; i++)
                             {
                                  int k = -1;
                                  string content31 = sfr2.ReadLine(); //построчное чтение файла, в котором обозначаются ссылки, которые участвуют в сравнении в конкретной строке 
                                  string content21 = str2.ReadLine();//построчное чтения файла, в котором отмечен порядок расположения ссылок
                                  for (j = 0; j < trs_in.Length; j++)
                                  {

                                      if (content31[j] == '1')
                                      {
                                           k = k + 1;
                                           int kar = content21[j];//ручной перевод чисел из Unicode в ASCII
                                           if (kar == 49)
                                                kar = 1;
                                           else kar = 2;
                                                b22[i, k] = kar; //запись в массив расположения ссылки (слево=1 или справо=2)
                                                a22[i, k] = random2[j];//запись в массив измененных весов
                                      }
                                  }
                             }
                             for (i = 0; i < kol; i++)
                             {
                                   if (a22[i, 0] < a22[i, 1]) //сравнение значений весов для конкретной строки
                                   {
                                        if ((b22[i, 0] == 1) || (b22[i, 1] == 2))//проверка порядка следования ссылок( сперва та, что слево (=1), потом та, что справо (=2)
                                        {
                                            test_outs[i, 0] = "right"; //тогда запись в конечный массив ответа
                                        }
                                        else//или наоборот
                                        if ((b22[i, 1] == 1) || (b22[i, 0] == 2))
                                        {
                                            test_outs[i, 0] = "left";
                                        }
                                   }
                                   else//аналогичные действия в случае, если второе значение веса ссылки не превосходит первое
                                   if (a22[i, 0] >= a22[i, 1])
                                   {
                                        if ((b22[i, 0] == 1) || (b22[i, 1] == 2))
                                        {
                                             test_outs[i, 0] = "left";
                                        }
                                        else
                                        if ((b22[i, 1] == 1) || (b22[i, 0] == 2))
                                        {
                                              test_outs[i, 0] = "right";
                                        }
                                   }
                                   else //в случае ошибки вывод соответствующих значений
                                   {
                                         test_outs[i, 0] = "error ! (" + a22[i, 0] + "( " + b22[i, 0] + ") , " + a22[i, 1] + " ( " + b22[i, 1] + ")";
                                   }
                             }
                        }
                        for (i = 0; i < kol; ++i)
                        {
                             for (j = 0; j < 1; ++j)
                             {
                                   swt.Write(test_outs[i, j] + " "); //запись массива ответов в файл ответов тестовой выборки
                            }
                             swt.WriteLine();
                        }
                        swt.WriteLine();
                    }
                }
                Console.WriteLine("Программа закончила работу");
        
               }
        }
    }
}
            

        
        
        
        
        
        
        
        
        
        
        
   
