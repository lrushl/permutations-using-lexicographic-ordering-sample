using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PermutationsUsingLexicographicOrderingSample
{
  class Program
  {
    static void Main(string[] args)
    {
      var source = new[] { 1, 2, 3 };
      var sw = Stopwatch.StartNew();
      foreach (var permutation in source.GeneratePermutations())
      {
        Console.WriteLine($"[{string.Join(",", permutation)}]");
      }
      Console.WriteLine($"Finished. Elapsed {sw.ElapsedMilliseconds}ms");
    }
  }

  public static class ArrayExtensions
  {

    /// <summary>
    /// Iterates through generated permutations
    /// </summary>
    /// <param name="source">The source.</param>
    /// <returns></returns>
    public static IEnumerable<int[]> GeneratePermutations(this int[] source)
    {
      var nextPermutation = new int[source.Length];
      Array.Copy(source, nextPermutation, source.Length);
      do
      {
        yield return nextPermutation;
      } while (ApplyPermutation(nextPermutation));
    }

    /// <summary>
    /// Generates permutations using lexicographic ordering.
    /// https://www.quora.com/How-would-you-explain-an-algorithm-that-generates-permutations-using-lexicographic-ordering
    /// </summary>
    /// <param name="source">The source.</param>
    /// <returns></returns>
    private static bool ApplyPermutation(int[] source)
    {
      // 1.Find the largest x such that P[x]<P[x+1].
      var largestX = -1;
      for (int x = 0; x < source.Length - 1; x++)
      {
        if (source[x] < source[x + 1])
        {
          largestX = x;
        }
      }
      if (largestX == -1)
      {
        return false;
      }

      // 2.Find the largest y such that P[x]<P[y].
      var largestY = -1;
      for (int y = largestX + 1; y < source.Length; y++)
      {
        if (source[largestX] < source[y])
        {
          largestY = y;
        }
      }
      if (largestY == -1)
      {
        return false;
      }

      // 3.Swap P[x] and P[y].
      Swap(ref source[largestX], ref source[largestY]);

      // 4.Reverse P[x+1 .. n].
      for (int x = largestX + 1, y = source.Length - 1; x < y; x++, y--)
      {
        Swap(ref source[x], ref source[y]);
      }

      return true;
    }

    /// <summary>
    /// XOR operator based swap of two int vaues without temp var.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    public static void Swap(ref int x, ref int y)
    {
      x ^= y;
      y ^= x;
      x ^= y;
    }
  }
}