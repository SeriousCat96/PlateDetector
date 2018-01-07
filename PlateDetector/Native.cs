//#define NO_AGGRESSIVE_INLINING
//#define ALLOW_UNSAFE
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Native
{
	/// <summary>Функции библиотеки kernel32.dll.</summary>
	[SuppressUnmanagedCodeSecurity]
	public static class Kernel32
	{
		/// <summary>Имя библиотеки.</summary>
		private const string LibName = "kernel32.dll";

		/// <summary>Точка входа функции CopyMemory.</summary>
		private const string CopyMemoryEntryPoint = "CopyMemory";

#if ALLOW_UNSAFE
		/// <summary>Copies a block of memory from one location to another.</summary>
		/// <param name="destination">A pointer to the starting address of the copied block's destination.</param>
		/// <param name="source">A pointer to the starting address of the block of memory to copy.</param>
		/// <param name="length">The size of the block of memory to copy, in bytes.</param>
		[DllImport(LibName, EntryPoint = CopyMemoryEntryPoint, SetLastError = false)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern unsafe void CopyMemory([In] byte* destination, [In] byte* source, [In] UIntPtr length);
#endif

		/// <summary>Copies a block of memory from one location to another.</summary>
		/// <param name="destination">A pointer to the starting address of the copied block's destination.</param>
		/// <param name="source">A pointer to the starting address of the block of memory to copy.</param>
		/// <param name="length">The size of the block of memory to copy, in bytes.</param>
		[DllImport(LibName, EntryPoint = CopyMemoryEntryPoint, SetLastError = false)]
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static extern void CopyMemory([In] IntPtr destination, [In] IntPtr source, [In] UIntPtr length);

		/// <summary>Copies a block of memory from one location to another.</summary>
		/// <param name="destination">A pointer to the starting address of the copied block's destination.</param>
		/// <param name="source">A pointer to the starting address of the block of memory to copy.</param>
		/// <param name="length">The size of the block of memory to copy, in bytes.</param>
#if !NO_AGGRESSIVE_INLINING
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		[SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
		public static void CopyMemory(IntPtr destination, IntPtr source, int length)
		{
			CopyMemory(destination, source, (UIntPtr)length);
		}
	}
}