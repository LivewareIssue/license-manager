Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.CompilerServices

Module Utilities
    ''' <summary>
    ''' Split an array into chunks of a given length.
    ''' </summary>
    ''' <typeparam name="item">The type of the elements of the array being split.</typeparam>
    ''' <param name="items">The array to split.</param>
    ''' <param name="chunkLength">The desired length of each chunk.</param>
    ''' <param name="padWith">The element with which to pad the last chunk, should the given array not divide evenly into chunks of the given size.</param>
    ''' <returns>Yields each chunk of the given array in turn.</returns>
    <Extension()>
    Public Iterator Function ChunksOf(Of item)(items As item(), chunkLength As Integer, Optional padWith As item = Nothing) As IEnumerable(Of item())
        Dim i As Integer = 0
        While i < items.Length
            If i + chunkLength > items.Length - 1 And Not IsNothing(padWith) Then
                Dim lastChunk As item() = Enumerable.Repeat(padWith, chunkLength).ToArray
                Array.Copy(items, i, lastChunk, 0, items.Length - i)
                Yield lastChunk
            Else
                Yield items.Skip(i).Take(chunkLength).ToArray
            End If

            i += chunkLength
        End While
    End Function

    <Extension()>
    Public Sub ThrowWhen(ex As Exception, condition As Boolean)
        If condition Then
            Throw ex
        End If
    End Sub
End Module
