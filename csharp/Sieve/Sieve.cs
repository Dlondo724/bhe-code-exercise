using System.Runtime.InteropServices;

namespace Sieve;

public interface ISieve
{
    long NthPrime(long n);
}

//---THE CHALLENGE---//
// Given the index position of a prime number return the prime number
//the 0 index of the challenge is the number 2

//---What are prime numbers?---//
// Prime numbers are numbers that are only divisible by 1 and itelf
// Even numbers can't be prime except for 2
// negative numbers can't be prime

//---How can this be accomplished--//
//NthPrime() is the main method. Its responsibility to drive, track and return the number at the desired index position

//--Orignially the code that I ported over from an exercise I had donne at a python bootcamp returned the 10000000, test at 17 minutes,
//after research through Geeks for Gekk, stack overflow and some other ebsites I was able to adapt code to return the required tests in 4s
//Math.log() wwas foreign to me and gave me some ne concept to research and understand better

public class SieveImplementation : ISieve
{
    public long NthPrime(long n)
    {
        long primeNumber = GetPrimeNumber(Convert.ToInt32(n));

        //return the prime number at the requested index position
        return primeNumber;
    }
    // Method to get the prime number at a specified index position
    public static long GetPrimeNumber(int index)
    {

        // Estimate the upper bound for the prime number at the given index
        long limit = EstimatePrimeAtIndex(index);

        // Generate prime numbers up to the estimated limit using Sieve of Eratosthenes
        List<long> primes = SieveOfEratosthenes(limit);

        // Return the prime number at the specified index
        if (index <= primes.Count)
        {
            return primes[index]; // nth prime is at index n-1 (0-indexed list)
        }
        else
        {
            throw new Exception("Prime number not found within the calculated limit");
        }
    }

    // Sieve of Eratosthenes to find all primes up to limit
    private static List<long> SieveOfEratosthenes(long limit)
    {
        //creates a boolean array starting off as all false (false = prime) up to the limit + 1
        bool[] sieve = new bool[limit + 1];
        //create a list that ill hold the prime numbers
        List<long> primes = new List<long>();

        //loop goes through the numbers and if false add the number to the list
        for (long i = 2; i <= limit; i++)
        {
            if (!sieve[i])
            {
                primes.Add(i);

                // go through the list and mark all the multiples ofthe number as true (composite number)
                for (long j = i * i; j <= limit; j += i)
                {
                    sieve[j] = true;
                }
            }
        }

        //return the primes list
        return primes;
    }

    // Estimate the upper bound for the nth prime number using prime number theorem
    private static long EstimatePrimeAtIndex(int n)
    {
        if (n < 6)
        {
            return 12; // For n < 6, the 5th prime is 11, so return a conservative estimate
        }
        else
        {
            double nn = n;
            return (long)(n * Math.Log(nn) + nn * Math.Log(Math.Log(nn))); // Prime number theorem approximation
        }
    }
}