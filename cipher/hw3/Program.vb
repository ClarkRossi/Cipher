Imports System
Imports System.IO
Imports System.Text

Module Program
    Sub Main(args As String())
        Dim inWords, cipherWords As String
        Dim allWords As String
        Dim bookWords(), searchWords() As String
        Dim cipherInput() As String
        Dim userChoice As Integer
        Dim cipherPath, bookPath As String
        Dim bookCatch, choiceCatch, findCatch, cipherCatch As Boolean

        'Creates file stream for cipher file
        Dim fileStream As FileStream

        'Creates array list to output word location
        Dim foundList As New ArrayList

        'Catches book file input 
        While Not bookCatch
            Try
                'Asks user to enter the text path 
                Console.WriteLine("Enter the book file path: ")
                bookPath = Console.ReadLine()

                'Reads in file to string
                allWords = File.ReadAllText(bookPath)

                bookCatch = True
            Catch e As FileNotFoundException
            Catch e As ArgumentException
            End Try
        End While

        'Removes returns because otherwise does some wierd stuff
        Dim noReturn As String = allwords.Replace(vbCrLf, " ")

        'Converts string file input to array
        noReturn = noReturn.ToLower
        bookWords = noReturn.Split(" "c)

        'Catch for userChoice
        While Not choiceCatch
            Try
                Console.WriteLine("Press 1 to create cipher or 2 to decipher: ")
                userChoice = Console.ReadLine()

                'Allows only 1 and 2 as input
                If userChoice > 0 And userChoice < 3 Then
                    choiceCatch = True
                End If
            Catch e As InvalidCastException
            End Try
        End While

        'Choice 1 create cipher
        If userChoice = 1 Then

            'Catches user input for finding words
            While Not findCatch
                Try
                    'Converts string input into array
                    Console.WriteLine("Enter words to find: ")
                    inWords = Console.ReadLine()
                    findCatch = True
                Catch e As InvalidCastException
                End Try
            End While

            inWords = inWords.ToLower
            searchWords = inWords.Split(" "c)

            'Compares arrays and adds matching indices to arraylist
            Dim sI, fI As Integer
            For sI = 0 To searchWords.Length - 1
                'Increments fI while not at end of array and match not found
                fI = 0
                While fI <> bookWords.Length - 1 And searchWords(sI) <> bookWords(fI)
                    fI += 1
                End While
                'If match is found add to list
                If searchWords(sI) = bookWords(fI) Then
                    foundList.Add(fI)
                Else
                    Console.WriteLine(searchWords(sI) & " not in text.")
                End If
            Next

            'Catches cipher creation path
            While Not cipherCatch
                Try
                    Console.WriteLine("Enter the cipher file path: ")
                    cipherPath = Console.ReadLine()

                    'Sets outPath to fileStream
                    FileStream = File.Create(cipherPath)

                    cipherCatch = True
                Catch e As FileNotFoundException
                Catch e As ArgumentException
                End Try
            End While

            Dim outText As Byte()

            'Prints each number from array list
            Dim fpI As Integer
            For fpI = 0 To foundList.Count - 1
                Console.WriteLine(foundList(fpI))
                outText = New UTF8Encoding(True).GetBytes(foundList(fpI) & " ")
                fileStream.Write(outText, 0, outText.Length)
            Next
            'Closes FileStream 
            fileStream.Close()

            'Choice 2 decipher the cipher 
        ElseIf userChoice = 2 Then

            While Not cipherCatch
                Try
                    Console.WriteLine("Enter the cipher file path: ")
                    cipherPath = Console.ReadLine()

                    'Reads cipher file into array
                    cipherWords = File.ReadAllText(cipherPath)

                    cipherCatch = True
                Catch e As FileNotFoundException
                Catch e As ArgumentException
                End Try
            End While

            'Removes spaces from cipherWords and enters into cipherInput array
            cipherInput = cipherWords.Split(" "c)

            Try
                'Compares arrays and prints matching indices
                Dim cI, bI As Integer
                For cI = 0 To cipherInput.Length - 1
                    'Increments bI while not at end of array and match not found
                    bI = 0
                    While bI <> bookWords.Length - 1 And cipherInput(cI) <> bI
                        bI += 1
                    End While
                    'If match is found print
                    If cipherInput(cI) = bI Then
                        Console.Write(bookWords(bI) & " ")
                    End If
                Next
            Catch e As InvalidCastException
            End Try
        End If
        Console.ReadKey()
    End Sub
End Module