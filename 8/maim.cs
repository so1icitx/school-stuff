using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;

namespace MathTasksSimpleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                PrintMenu();

                int choice = ReadInt("Choose option (1-8) or 0 to exit: ");
                Console.WriteLine();

                if (choice == 0) return;

                try
                {
                    switch (choice)
                    {
                        case 1: Task1_BaseConversion(); break;
                        case 2: Task2_MeanModeMedian(); break;
                        case 3: Task3_Determinant3x3(); break;
                        case 4: Task4_PolynomialRoots(); break;
                        case 5: Task5_ShortestVector3D(); break;
                        case 6: Task6_SetOperations(); break;
                        case 7: Task7_Combinatorics(); break;
                        case 8: Task8_DiceSimulation(); break;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }

                Console.WriteLine();
                Console.WriteLine("Press ENTER to return to menu...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        static void PrintMenu()
        {
            Console.WriteLine("====================================");
            Console.WriteLine(" SIMPLE C# CONSOLE PROJECT (8 tasks) ");
            Console.WriteLine("====================================");
            Console.WriteLine("1) Convert number between bases");
            Console.WriteLine("2) Mean / Mode / Median of a list");
            Console.WriteLine("3) Determinant of a 3x3 matrix");
            Console.WriteLine("4) Roots of a polynomial P(x)=0");
            Console.WriteLine("5) Shortest 3D vector (length + vector)");
            Console.WriteLine("6) Set operations (intersection/union/difference)");
            Console.WriteLine("7) Permutations / Combinations / Variations");
            Console.WriteLine("8) Dice probability simulation (100000 throws)");
            Console.WriteLine("------------------------------------");
        }

        static void Task1_BaseConversion()
        {
            Console.WriteLine("TASK 1: Base conversion");
            Console.WriteLine("Enter number, from base, and to base. Bases: 2..36");
            Console.WriteLine();

            string numberText = ReadNonEmpty("Number: ").Trim().ToUpperInvariant();
            int fromBase = ReadInt("From base: ");
            int toBase = ReadInt("To base: ");

            if (fromBase < 2 || fromBase > 36 || toBase < 2 || toBase > 36)
                throw new Exception("Base must be between 2 and 36.");

            long decimalValue = ToDecimal(numberText, fromBase);
            string result = FromDecimal(decimalValue, toBase);

            Console.WriteLine();
            Console.WriteLine($"Result: {result}");
        }

        static long ToDecimal(string s, int fromBase)
        {
            string digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            s = s.Trim().ToUpperInvariant();
            bool negative = false;

            if (s.StartsWith("-"))
            {
                negative = true;
                s = s.Substring(1);
            }

            if (s.Length == 0)
                throw new Exception("Empty number.");

            long result = 0;

            for (int i = 0; i < s.Length; i++)
            {
                char ch = s[i];
                int value = digits.IndexOf(ch);

                if (value < 0 || value >= fromBase)
                    throw new Exception($"Invalid digit '{ch}' for base {fromBase}.");

                checked
                {
                    result = result * fromBase + value;
                }
            }

            return negative ? -result : result;
        }

        static string FromDecimal(long value, int toBase)
        {
            string digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            if (value == 0)
                return "0";

            bool negative = value < 0;
            long n = value;

            if (negative) n = -n;

            List<char> resultChars = new List<char>();

            while (n > 0)
            {
                int rem = (int)(n % toBase);
                resultChars.Add(digits[rem]);
                n = n / toBase;
            }

            resultChars.Reverse();
            string result = new string(resultChars.ToArray());

            return negative ? "-" + result : result;
        }

        static void Task2_MeanModeMedian()
        {
            Console.WriteLine("TASK 2: Mean / Mode / Median");
            Console.WriteLine("Enter numbers separated by spaces.");
            Console.WriteLine("Example: 5 3 5");
            Console.WriteLine();

            double[] nums = ReadDoubleArray("Numbers: ", expectedCount: -1);
            if (nums.Length == 0)
                throw new Exception("You must enter at least one number.");

            double sum = 0;
            for (int i = 0; i < nums.Length; i++) sum += nums[i];
            double mean = sum / nums.Length;

            double[] sorted = (double[])nums.Clone();
            Array.Sort(sorted);
            double median;
            if (sorted.Length % 2 == 1)
            {
                median = sorted[sorted.Length / 2];
            }
            else
            {
                int mid = sorted.Length / 2;
                median = (sorted[mid - 1] + sorted[mid]) / 2.0;
            }

            Dictionary<double, int> counts = new Dictionary<double, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                double key = Math.Round(nums[i], 10);
                if (!counts.ContainsKey(key)) counts[key] = 0;
                counts[key]++;
            }

            int maxCount = 0;
            foreach (var kv in counts)
            {
                if (kv.Value > maxCount) maxCount = kv.Value;
            }

            List<double> modes = new List<double>();
            foreach (var kv in counts)
            {
                if (kv.Value == maxCount) modes.Add(kv.Key);
            }
            modes.Sort();

            Console.WriteLine();
            Console.WriteLine("Mean   = " + FormatNumber(mean));
            Console.WriteLine("Median = " + FormatNumber(median));

            if (maxCount == 1)
            {
                Console.WriteLine("Mode   = (no mode, all values occur once)");
            }
            else
            {
                Console.Write("Mode   = ");
                for (int i = 0; i < modes.Count; i++)
                {
                    if (i > 0) Console.Write(", ");
                    Console.Write(FormatNumber(modes[i]));
                }
                Console.WriteLine($" (each occurs {maxCount} times)");
            }
        }

        static void Task3_Determinant3x3()
        {
            Console.WriteLine("TASK 3: Determinant of 3x3 matrix");
            Console.WriteLine("Enter 3 rows, each with 3 numbers separated by spaces.");
            Console.WriteLine();

            double[,] m = new double[3, 3];

            for (int r = 0; r < 3; r++)
            {
                double[] row = ReadDoubleArray($"Row {r + 1}: ", expectedCount: 3);
                for (int c = 0; c < 3; c++)
                    m[r, c] = row[c];
            }

            double a = m[0, 0], b = m[0, 1], c1 = m[0, 2];
            double d = m[1, 0], e = m[1, 1], f = m[1, 2];
            double g = m[2, 0], h = m[2, 1], i1 = m[2, 2];

            double det = a * (e * i1 - f * h)
                       - b * (d * i1 - f * g)
                       + c1 * (d * h - e * g);

            Console.WriteLine();
            Console.WriteLine("Determinant = " + FormatNumber(det));
        }

        static void Task4_PolynomialRoots()
        {
            Console.WriteLine("TASK 4: Roots of polynomial P(x)=0");
            Console.WriteLine("Enter coefficients (highest degree first) separated by spaces.");
            Console.WriteLine("Example: 3 2 -16  means  3x^2 + 2x - 16");
            Console.WriteLine();

            double[] a = ReadDoubleArray("Coefficients: ", expectedCount: -1);
            if (a.Length == 0)
                throw new Exception("You must enter coefficients.");

            int start = 0;
            while (start < a.Length && Math.Abs(a[start]) < 1e-12) start++;
            if (start == a.Length)
            {
                Console.WriteLine("P(x) = 0 for all x (all coefficients are zero).");
                return;
            }
            if (start > 0)
            {
                double[] b = new double[a.Length - start];
                for (int j = 0; j < b.Length; j++) b[j] = a[start + j];
                a = b;
            }

            int degree = a.Length - 1;

            if (degree == 0)
            {
                Console.WriteLine("Constant polynomial. No roots unless constant is 0.");
                return;
            }

            if (degree == 1)
            {
                double A = a[0];
                double B = a[1];
                if (Math.Abs(A) < 1e-12)
                    throw new Exception("Not a valid linear polynomial (a=0).");

                double x = -B / A;
                Console.WriteLine("Root: x = " + FormatNumber(x));
                return;
            }

            if (degree == 2)
            {
                double A = a[0], B = a[1], C = a[2];

                if (Math.Abs(A) < 1e-12)
                {
                    if (Math.Abs(B) < 1e-12)
                        throw new Exception("Not a valid polynomial.");
                    double x = -C / B;
                    Console.WriteLine("Root: x = " + FormatNumber(x));
                    return;
                }

                double D = B * B - 4 * A * C;

                if (D > 1e-12)
                {
                    double sqrtD = Math.Sqrt(D);
                    double x1 = (-B + sqrtD) / (2 * A);
                    double x2 = (-B - sqrtD) / (2 * A);

                    Console.WriteLine("Two real roots:");
                    Console.WriteLine("x1 = " + FormatNumber(x1));
                    Console.WriteLine("x2 = " + FormatNumber(x2));
                }
                else if (Math.Abs(D) <= 1e-12)
                {
                    double x = -B / (2 * A);
                    Console.WriteLine("One real root (double):");
                    Console.WriteLine("x = " + FormatNumber(x));
                }
                else
                {
                    Console.WriteLine("No real roots (discriminant < 0).");
                }

                return;
            }

            Console.WriteLine($"Degree = {degree}. We'll try to find REAL roots numerically.");
            Console.WriteLine("Note: This simple method may miss repeated roots or complex roots.");
            Console.WriteLine();

            List<double> roots = FindRealRootsByScanning(a);
            if (roots.Count == 0)
            {
                Console.WriteLine("No real roots found (with scanning).");
            }
            else
            {
                Console.WriteLine("Real roots found:");
                for (int r = 0; r < roots.Count; r++)
                {
                    Console.WriteLine($"x{r + 1} = " + FormatNumber(roots[r]));
                }
            }
        }

        static List<double> FindRealRootsByScanning(double[] a)
        {
            double a0 = a[0];
            double max = 0;
            for (int i = 1; i < a.Length; i++)
            {
                double v = Math.Abs(a[i] / a0);
                if (v > max) max = v;
            }
            double R = 1 + max;

            int steps = 20000;
            double step = (2 * R) / steps;

            List<double> roots = new List<double>();

            double x1 = -R;
            double y1 = EvalPoly(a, x1);

            for (int s = 1; s <= steps; s++)
            {
                double x2 = -R + s * step;
                double y2 = EvalPoly(a, x2);

                if (Math.Abs(y1) < 1e-8)
                    AddRootIfNew(roots, x1);

                if (y1 * y2 < 0)
                {
                    double root = Bisection(a, x1, x2);
                    AddRootIfNew(roots, root);
                }

                x1 = x2;
                y1 = y2;
            }

            if (Math.Abs(y1) < 1e-8)
                AddRootIfNew(roots, x1);

            roots.Sort();
            return roots;
        }

        static void AddRootIfNew(List<double> roots, double x)
        {
            for (int i = 0; i < roots.Count; i++)
            {
                if (Math.Abs(roots[i] - x) < 1e-4)
                    return;
            }
            roots.Add(x);
        }

        static double Bisection(double[] a, double left, double right)
        {
            double fL = EvalPoly(a, left);
            double fR = EvalPoly(a, right);

            if (fL == 0) return left;
            if (fR == 0) return right;
            if (fL * fR > 0) return (left + right) / 2;

            for (int i = 0; i < 100; i++)
            {
                double mid = (left + right) / 2;
                double fM = EvalPoly(a, mid);

                if (Math.Abs(fM) < 1e-10) return mid;

                if (fL * fM < 0)
                {
                    right = mid;
                    fR = fM;
                }
                else
                {
                    left = mid;
                    fL = fM;
                }
            }

            return (left + right) / 2;
        }

        static double EvalPoly(double[] a, double x)
        {
            double y = 0;
            for (int i = 0; i < a.Length; i++)
            {
                y = y * x + a[i];
            }
            return y;
        }

        static void Task5_ShortestVector3D()
        {
            Console.WriteLine("TASK 5: Shortest 3D vector");
            Console.WriteLine("Enter N (count), then N vectors as: x y z");
            Console.WriteLine();

            int n = ReadInt("N: ");
            if (n <= 0) throw new Exception("N must be > 0.");

            double bestLen = double.MaxValue;
            double bestX = 0, bestY = 0, bestZ = 0;

            for (int i = 0; i < n; i++)
            {
                double[] v = ReadDoubleArray($"Vector {i + 1}: ", expectedCount: 3);
                double x = v[0], y = v[1], z = v[2];
                double len = Math.Sqrt(x * x + y * y + z * z);

                if (len < bestLen)
                {
                    bestLen = len;
                    bestX = x; bestY = y; bestZ = z;
                }
            }

            Console.WriteLine();
            Console.WriteLine("Shortest length = " + FormatNumber(bestLen));
            Console.WriteLine($"Vector = ({FormatNumber(bestX)}, {FormatNumber(bestY)}, {FormatNumber(bestZ)})");
        }

        static void Task6_SetOperations()
        {
            Console.WriteLine("TASK 6: Set operations");
            Console.WriteLine("Enter two sets of integers separated by spaces.");
            Console.WriteLine("Example A: 1 2 3");
            Console.WriteLine("Example B: 3 4 5");
            Console.WriteLine();

            int[] aArr = ReadIntArray("Set A: ", expectedCount: -1);
            int[] bArr = ReadIntArray("Set B: ", expectedCount: -1);

            HashSet<int> A = new HashSet<int>(aArr);
            HashSet<int> B = new HashSet<int>(bArr);

            HashSet<int> intersection = new HashSet<int>(A);
            intersection.IntersectWith(B);

            HashSet<int> union = new HashSet<int>(A);
            union.UnionWith(B);

            HashSet<int> diffA = new HashSet<int>(A);
            diffA.ExceptWith(B);

            HashSet<int> diffB = new HashSet<int>(B);
            diffB.ExceptWith(A);

            Console.WriteLine();
            Console.WriteLine("Intersection (A ∩ B): " + JoinSet(intersection));
            Console.WriteLine("Union (A ∪ B):        " + JoinSet(union));
            Console.WriteLine("Difference (A \\ B):   " + JoinSet(diffA));
            Console.WriteLine("Difference (B \\ A):   " + JoinSet(diffB));
        }

        static string JoinSet(HashSet<int> set)
        {
            List<int> list = new List<int>(set);
            list.Sort();

            if (list.Count == 0) return "(empty)";

            string s = "";
            for (int i = 0; i < list.Count; i++)
            {
                if (i > 0) s += " ";
                s += list[i].ToString();
            }
            return s;
        }

        static void Task7_Combinatorics()
        {
            Console.WriteLine("TASK 7: Permutations / Combinations / Variations");
            Console.WriteLine("Enter n and k (two integers).");
            Console.WriteLine("We output: n! , C(n,k) , V(n,k) where V(n,k)=n!/(n-k)!");
            Console.WriteLine("Example: n=5, k=2 => 120 10 20");
            Console.WriteLine();

            int n = ReadInt("n: ");
            int k = ReadInt("k: ");

            if (n < 0) throw new Exception("n must be >= 0.");
            if (k < 0) throw new Exception("k must be >= 0.");
            if (k > n) throw new Exception("k must be <= n.");

            BigInteger perm = Factorial(n);
            BigInteger comb = Combination(n, k);
            BigInteger var = Variation(n, k);

            Console.WriteLine();
            Console.WriteLine("n!      = " + perm);
            Console.WriteLine("C(n,k)  = " + comb);
            Console.WriteLine("V(n,k)  = " + var);
        }

        static BigInteger Factorial(int n)
        {
            BigInteger r = 1;
            for (int i = 2; i <= n; i++)
                r *= i;
            return r;
        }

        static BigInteger Combination(int n, int k)
        {
            if (k > n - k) k = n - k;

            BigInteger num = 1;
            BigInteger den = 1;

            for (int i = 1; i <= k; i++)
            {
                num *= (n - (k - i));
                den *= i;
            }

            return num / den;
        }

        static BigInteger Variation(int n, int k)
        {
            BigInteger result = 1;
            for (int i = 0; i < k; i++)
                result *= (n - i);
            return result;
        }

        static void Task8_DiceSimulation()
        {
            Console.WriteLine("TASK 8: Dice probability simulation");
            Console.WriteLine("We simulate throwing a die many times and estimate probabilities.");
            Console.WriteLine();

            int throws = ReadInt("Number of throws (default 100000): ", allowEmpty: true, defaultValue: 100000);
            if (throws <= 0) throw new Exception("Number of throws must be > 0.");

            int[] count = new int[7];
            Random rnd = new Random();

            for (int i = 0; i < throws; i++)
            {
                int face = rnd.Next(1, 7);
                count[face]++;
            }

            Console.WriteLine();
            for (int face = 1; face <= 6; face++)
            {
                double p = (double)count[face] / throws;
                Console.WriteLine($"{face}: count={count[face]}, p≈{p.ToString("0.######", CultureInfo.CurrentCulture)}");
            }
        }

        static int ReadInt(string prompt, bool allowEmpty = false, int defaultValue = 0)
        {
            while (true)
            {
                Console.Write(prompt);
                string line = Console.ReadLine();

                if (allowEmpty && string.IsNullOrWhiteSpace(line))
                    return defaultValue;

                if (int.TryParse(line, out int value))
                    return value;

                Console.WriteLine("Please enter a valid integer.");
            }
        }

        static string ReadNonEmpty(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(s)) return s;
                Console.WriteLine("Please enter something.");
            }
        }

        static double[] ReadDoubleArray(string prompt, int expectedCount)
        {
            while (true)
            {
                Console.Write(prompt);
                string line = Console.ReadLine();
                double[] arr = ParseDoubles(line);

                if (expectedCount > 0 && arr.Length != expectedCount)
                {
                    Console.WriteLine($"Please enter exactly {expectedCount} numbers.");
                    continue;
                }

                if (expectedCount < 0 && arr.Length == 0)
                {
                    Console.WriteLine("Please enter at least one number.");
                    continue;
                }

                return arr;
            }
        }

        static int[] ReadIntArray(string prompt, int expectedCount)
        {
            while (true)
            {
                Console.Write(prompt);
                string line = Console.ReadLine();
                int[] arr = ParseInts(line);

                if (expectedCount > 0 && arr.Length != expectedCount)
                {
                    Console.WriteLine($"Please enter exactly {expectedCount} integers.");
                    continue;
                }

                if (expectedCount < 0 && arr.Length == 0)
                {
                    Console.WriteLine("Please enter at least one integer.");
                    continue;
                }

                return arr;
            }
        }

        static double[] ParseDoubles(string line)
        {
            if (line == null) return new double[0];

            line = line.Trim();
            if (line.Length == 0) return new double[0];

            line = line.Replace(',', '.');
            line = line.Replace(";", " ");
            line = line.Replace("\t", " ");

            string[] parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            double[] values = new double[parts.Length];
            for (int i = 0; i < parts.Length; i++)
            {
                values[i] = double.Parse(parts[i], CultureInfo.InvariantCulture);
            }

            return values;
        }

        static int[] ParseInts(string line)
        {
            if (line == null) return new int[0];

            line = line.Trim();
            if (line.Length == 0) return new int[0];

            line = line.Replace(',', ' ');
            line = line.Replace(';', ' ');
            line = line.Replace("\t", " ");

            string[] parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            int[] values = new int[parts.Length];
            for (int i = 0; i < parts.Length; i++)
            {
                values[i] = int.Parse(parts[i]);
            }

            return values;
        }

        static string FormatNumber(double x)
        {
            return x.ToString("0.######", CultureInfo.CurrentCulture);
        }
    }
}
