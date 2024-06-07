using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication4
{
    class program //بخش اصلی اجرای برنامه
    {
        public static Skill StringToSkill(string input) //گرفتن یه ورودی استرینگ به نام مهارت و برگرداندن مهارت متناضر با اون
        {
            if (input == "A") return Skill.A; 
            else if (input == "B") return Skill.B;
            else if (input == "C") return Skill.C;
            else if (input == "D") return Skill.D;
            else if (input == "E") return Skill.E;
            else if (input == "F") return Skill.F;
            else throw new ArgumentException("invalid input");//اگه غیر اینا باشه میگه ورودب نامعتبره
        }
        static void Main(string[] args) // تعریف تابع اصلی برنامه که همیشه با این نام در هر برنامه سی شارپ استفاده میشود این تابع ارایه ای از رشته ها به نام ارگز رو به عنوان ورودی میگیره
        {

            MaxPriorityQueue queue = new MaxPriorityQueue(); //ایجاد یه نمونه صف الویت به نام: queue
            while (true) //یه حلقه ی بینهایت تا زمانی که شرط صحیح باشد ادامه دارد
            {
                //چاپ گزینه ی ممکن برای کاربر و درخواست ورود عددی از ورودی
                Console.WriteLine("1-insert new person\n2-choose best person and pop\n3-raise a person's skill\n4-show queue\nenter a number:"); 
        
                int input = int.Parse(Console.ReadLine()); //خواندن عدد ورودی از کاربر و تبدیل ان به عدد صحیح
                Console.Clear(); //پاک کردن محتویات صفحه کنسول
                switch (input) //شروع یه ساختار سوییچ برای بررسی مقدار ورودی
                {
                    case 1: // بررسی اگه مقدار 1 بود
                        {
                            Console.Write("enter age: "); //نمایش پیام برای وارد کردن سن 
                            int age = int.Parse(Console.ReadLine()); //خواندن ورودی سن و تبدیل ان به عدد صحیح
                            Console.Write("enter skill (A, B, C, D, E, F): "); //نمایش پیام برای وارد کردن سطح مهارت
                            Skill skill = StringToSkill(Console.ReadLine()); //خواندن ورودی سطح مهارت و تبدیل ان به مهارت معادل با استفاده از تابع:stringtoskill
                            queue.insert(new Person(age, skill)); //وارد کردن یه شخص جدید به صف با تابع: insert
                            Console.WriteLine("person added to the queue"); //چاپ پیام شخص جدید به صف اضافه شد
                            break;
                        }
                    case 2: //بررسی اگه مقدار ورودی 2 بود
                        {
                            Console.Write("top person is: "); //نمایش پیام برای  شخص تاپ
                            Person top = queue.extractMax(); //خارج کردن فرد با بیشترین مهارت از صف و ذخیره ان در متغیر :top
                            Console.WriteLine("[age: {0}, skill: {1}]", top.age, top.skill); //نمایش  سن و مهارت فرد برتر
                            break;
                        }
                    case 3: //بررسی اگه مقدار ورودی 3 بود
                        {
                            Console.Write("enter person's index: "); //وارد کردن موقعیت فردی که میخوایم سطح مهارتش رو تغییر بدیم
                            int index = int.Parse(Console.ReadLine()); //
                            Console.Write("enter new skill level: "); //سطح مهارت جدید رو کاربر وارد میکنه
                            Skill skill = StringToSkill(Console.ReadLine()); //
                            queue.raiseSkill(index, skill); //با این تابع سطح مهارت فرد مربوطه در صف اصلاح میشه
                            Console.WriteLine("person's skill raised"); //به کاربر اعلام میشه که سطح مهارت تغییر کرده
                            break;
                        }
                    case 4: //بررسی اگه مقدار ورودی 4 بود
                        {
                            queue.print(); //محتوای کل صف الویت چاپ میشود
                            // این کار با استفاده از حلقه ای تمامی اعضای صف را طی کرده و سپس هر عضو رو چاپ میکنه انجام میشه
                            //پس از چاپ تمامی اعضا برنامه ادامه میابد
                            break;
                        }
                }
            }
        }
    }
       internal class MaxPriorityQueue //پیاده سازی صف الویت با استفاده از یه ارایه
    {
        Person[] heap; // ارایه اصلی اعضا یا همون اطلاعات اعضا
        int size; //اندازه فعلی صف یا همون تعداد افراد در صف
        public MaxPriorityQueue()
        { 
            heap = new Person[100]; //یه سازنده بدون پارامتر که ارایه هیپ رو با اندازه 100 ایجاد میکنه
            size = -1; //مقدار اولیه سایز به منفی یک مقدار دهی اولیه میشود
        }

        static int left(int n) //static method(فرزند چپ)
        {
            return n * 2 + 1;
        }
        static int right(int n) //static method(فرزند راست)
        {
            return n * 2 + 2;
        }
        static int parent(int n) //static method(والد)
        {
            return (n - 1) / 2;
        }
        void maxHeapify(int i) //این تابع برای حفظ ویژگی های ماکس هیپ
        {
            int maxIndex = i; //در ابتدا عنصر فعلی به عنوان عنصر حاکم در نظر گرفته میشه(i)

            int l = left(i);
            int r = right(i);

            if (l <= size && heap[l].skill > heap[maxIndex].skill) //اگه عنصر چپ وجود داشته باشد و چپ بزرگتر از ماکس هیپ فعلی باشه جاشون عوض میشه 
            {
                maxIndex = l; //پس اگه هر دو شرط درست باشه چپ به عنوان ماکس هیپ هست
            }
            if (r <= size && heap[r].skill > heap[maxIndex].skill) //اگه عنصر راست وجود داشته باشد و راست بزرگتر از ماکس هیپ فعلی باشه جاشون عوض میشه 
            {
                maxIndex = r; //پس اگه هر دو شرط درست باشه راست به عنوان ماکس هیپ هست
            }
            if (i != maxIndex) //چک میشه که ایا عنصر فعلی بزرگترین عنصر در زیر درخت خودش است یا نه
            {
                swap(i, maxIndex); //اگه گره فعلی که همون پرنت هست بزرگترین نباشه بین 3 تا گره که مقایسه کردیم باید جاشو با اون فرزندی که بزرگتر بود عوض کنه
                maxHeapify(maxIndex); //farakhani mojadad maxHeapify bar royr shakhes maxindex, moratabsazi derakht hadaksari edame peyda mikone.
            }
        }
        void shiftUp(int i) //هر گره باید از والدش کوچکتر باشد 
        {
            while (i > 0 && (int)heap[parent(i)].skill < (int)heap[i].skill) //اگه اندیس فعلی بزرگتر از صفر باشدو همچنین مقدار مهارت یا همون اسکیل فرزند بزرگتر از مقدار مهارت والدین باشد 
            {
                swap(parent(i), i); //اگه هر دو صحیح باشد عملیات جا به جایی یا همون سواپ انجام میشود
                i = parent(i); //پس گره فعلی والد میشود
            }
        }
        void swap(int i, int j) //دو تا اندیس میگیره و جاشون رو عوض میکنه
        {
            Person temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }
        public Person extractMax() //از ابتدای صف بزرگترین رو حذف و برمیگردونه
        {
            if (size == -1) //صف خالی است
                throw new InvalidOperationException("Queue is empty"); //صف خالیه
            Person result = heap[0]; //عنصر اول یعنی بزرگترین ارایه هیپ رو در متغیر ریزالت ذخیره میکنه
            heap[0] = heap[size]; //عنصر اخر یعنی کوچکترین رو به جایگاه بزرگترین یعنی اولین منتقل میکنه
            size = size - 1; //از سایز ارایه یدونه کم میکنه که عنصر اخرین  حذف شده از ارایه حذف بشه
            maxHeapify(0); //بازسازی ویژگی درخت باینریحاوی داده انجام میده.تا ویژگی صف مرتب مجددا حفظ شود
            return result; //مقدار عنصر بزرگترین که از صف حذف شده روبرمیگردونه
        }
        public void insert(Person p) //اضافه کردن عنصر جدید به صف
        {
            if (size == heap.Length - 1) //صف پر است یا نه
                throw new InvalidOperationException("Queue is full");//اگه شرط درست باشه میگه صف پره
            size = size + 1; //اندازه صف افزایش میابد تا یک خانه جدید برای عنصر جدید فراهم شود
            heap[size] = p; //عنصر جدید پی به صف اضافه میشه و در انتهای صف در اندیس سایز قرار میگیره
            shiftUp(size); //تابع شیفت رو صدا میزنه تا ویژگی صف رو بازسازی کنه وعنصر اضافه شده روبه مکان مناسب در صف منتقل کنه
        }
        public void raiseSkill(int i, Skill p) ////افزایش مهارت یه شخص
        {
            if ((int)p < (int)heap[i].skill) //ایا مهارت جدید کمتر از مهارت فعلی شخص است یا نه
                throw new InvalidOperationException("New skill is lower than previous skill"); //مهارت جدید کمتر از مهارت فعلی است
            if (i > size) // شماره ایندکس بزرگتر از محدوده صف است
                throw new InvalidOperationException("index is out of heap's bounds"); //شماره ی ایندکس خارج از محدوده ی صف است

            heap[i].skill = p; //اگه هر دو شرط فالز باشد مهارت شخص در ارایه صف به مهارت جدید اضافه میشود و سپس با فراخوانی تابه شیفت اپ از روی ایندکس مورد نظر صف بازسازی میشود
            shiftUp(i);
        }

        // print():  برای چاپ محتوای صف الویت به صورت یک لیست فرمت دهی شده به کار میره
        public void print() //void:مقدار بازگشتی نداره
        {
            Console.Write("{"); //چاپ در خروجی برای شروع لیست
            for (int i = 0; i <= size; i++) // شروع حلقه ی تکرار برای گرداوری اعضای صف الویت ..این حلقه از اول تا اخررین اعضای صف الویت رو پیمایش میکنه
            {
                Console.Write("[age: {0}, skill: {1}], ", heap[i].age, heap[i].skill); //چاپ مشخصات هر عضو صف الویت به صورت سن و سطح مهارت
            }
            Console.WriteLine("}"); //برای نمایش پایان لیست
        }
    }
       enum Skill  //تعریف یه نوع شمایلی از داده
    {
        A, B, C, D, E, F //مجموعه ای از مقادیر ثابت
    }

    internal class Person //فقط دسترسی داخلی  و در این مجموعه برنامه در دسترس است
    {
        public int age; //متغیر عمومی
        public Skill skill; //متغیر عمومی

        public Person(int age, Skill skill) // تابع سازنده برای این کلاس شخص که 2 پارامتر میگیره
        {
            this.age = age; // تعیین سن فرد با مقداری که به عنوان ورودی به تابع سازنده داده میشود
            this.skill = skill; //تعیین مقدار سطح مهارت فرد با مقداری که به عنوان ورودی به تابع سازنده داده میشود
        }
    }
}
