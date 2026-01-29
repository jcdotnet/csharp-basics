//  Memory management is handled by the Common Language Runtime (CLR)
using ClassLibrary;

var person = new Person() { FirstName = "John", LastName = "Doe" }; // pointer to person

unsafe
{
    int number = 10;
    int* p = &number;                   // internal pointer to number 
    Console.WriteLine(*p);              // outputs 10 (value of number)
    Console.WriteLine($"{(ulong)p:X}"); // outputs B62BB9E7A4 (memory address of number)

    int[] emptyArray1 = [];
    int[] emptyArray2 = [];

    Console.WriteLine($"emptyArray1 == emptyArray22: {emptyArray1 == emptyArray2}"); // True

    fixed (int* ptr1 = emptyArray1) // internal pointer (holds the memory address of emptyArray1)
    {
        Console.WriteLine((ulong)ptr1); // outputs 0 // null
    }

    fixed (int* ptr2 = emptyArray2) // internal pointer (holds the memory address of emptyArray2)
    {
        Console.WriteLine((ulong)ptr2); // outputs 0 // null
    }

    int[] numbers1 = [10, 20, 30, 40, 50];
    int[] numbers2 = [10, 20, 30, 50];
    int[] numbers3 = [10, 20, 30, 40, 50];
    int[] numbers4 = numbers3;

    Console.WriteLine($"numbers1 == numbers2: {numbers1 == numbers2}"); // False
    Console.WriteLine($"numbers1 == numbers3: {numbers1 == numbers3}"); // False
    Console.WriteLine($"numbers3 == numbers4: {numbers3 == numbers4}"); // True
    Console.WriteLine($"numbers1 == numbers4: {numbers1 == numbers4}"); // False

    fixed (int* ptrNumbers1 = numbers1) // internal pointer to numbers1 first element
    {
        Console.WriteLine($"numbers1 memory address: {(ulong)ptrNumbers1:X}"); 
        // outputs 218718FCCD0

        //for (int i = 0; i < numbers1.Length; i++)
        //{
        //    // Console.WriteLine(ptrNumbers1[i]);
        //}
    }

    fixed (int* ptrNumbers1elem1 = &numbers1[0]) // internal pointer to numbers1 first element also
    {
        Console.WriteLine($"numbers1 memory address of first element: {(ulong)ptrNumbers1elem1:X}");
        // outputs 218718FCCD0 also 
    }

    fixed (int* ptrNumbers2 = numbers2) // internal pointer to numbers2 first element
    {
        Console.WriteLine($"numbers2 memory address: {(ulong)ptrNumbers2:X}"); 
        // outputs 218718FCD48
    }

    fixed (int* ptrNumbers3 = numbers3) // internal pointer to numbers3 first element
    {
        Console.WriteLine($"numbers3 memory address: {(ulong)ptrNumbers3:X}");
        // outputs 218718FCDB8
    }

    fixed (int* ptrNumbers4 = numbers4) // internal pointer to numbers4 first element
    {
        Console.WriteLine($"numbers4 memory address: {(ulong)ptrNumbers4:X}");
        // outputs 218718FCDB8 also
    }
}