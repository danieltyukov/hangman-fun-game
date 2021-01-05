Imports System
Module Program
    Private wordlist As New List(Of String)
    Private r As New Random
    Sub Main()
        Call AddWords()

        Do Until True = False
            Dim guessedword As String = wordlist.Item(r.Next(0, wordlist.Count))
            Dim gamemode As String

            Dim h As New Hangman


            h.TextWord = guessedword
            gamemode = h.Difficulty()

            Do Until h.ShowRightLeg = True Or h.WordAlter.Contains("_") = False
                Console.WriteLine("WELCOME TO THE HANG MAN GAME")
                Console.WriteLine("----------------------------")
                Console.WriteLine(h.Draw)
                Console.WriteLine()
                If gamemode = "A" Then
                    Console.WriteLine(guessedword.Substring(0, 1) & " " & h.WordAlter & guessedword.Substring(guessedword.Length - 1, 1))
                Else
                    Console.WriteLine(h.WordAlter)
                End If
                Console.WriteLine()
                Console.Write("Enter a letter: ")

                Dim response As String = Console.ReadLine.ToLower

                If response.Length > 1 Or response.Length = 0 Or IsNumeric(response) Then
                    Console.WriteLine("It has to be just 1 normal letter")
                Else
                    If h.Guessed.Contains(response) Then
                        Console.WriteLine("We already used this letter " & response)
                        Console.WriteLine("Press anything to continue the game")
                        Console.ReadLine()
                    Else
                        Dim checker As Boolean = False
                        For i As Integer = 0 To h.TextWord.Length - 1
                            If h.TextWord.Substring(i, 1) = response Then
                                checker = False
                                Exit For
                            Else
                                checker = True
                            End If
                        Next

                        If checker = True Then
                            h.Checker()
                        End If

                        h.Guessed.Add(response)
                    End If

                    If h.ShowRightLeg = True Then
                        Console.WriteLine(h.Draw)
                        Console.WriteLine()
                        Console.WriteLine("Sorry, but you lose.")
                        Console.WriteLine("The word was: " & h.TextWord)
                        Console.WriteLine("Press any key to continue...")
                        Console.ReadLine()
                    ElseIf h.WordAlter.Contains("_") = False Then
                        If gamemode = "A" Then
                            Console.WriteLine(guessedword.Substring(0, 1) & " " & h.WordAlter & guessedword.Substring(guessedword.Length - 1, 1))
                        Else
                            Console.WriteLine(h.WordAlter)
                        End If
                        Console.WriteLine()
                        Console.WriteLine("You guessed correctly, you win!")
                        Console.WriteLine("Press any key to continue...")
                        Console.ReadLine()
                    End If
                End If

                Console.Clear()
            Loop

        Loop
    End Sub
    Private Sub AddWords()
        wordlist.AddRange({"apple", "rockets", "elephant", "george", "networking", "physics", "computer", "maths", "formulas", "daniel", "programming", "database", "aspire", "christmas", "birthday"})
    End Sub

End Module

Public Class Hangman
    Private word As String = String.Empty
    Private guessedlet As New List(Of String)
    Private head As Boolean = False
    Private body As Boolean = False
    Private leftArm As Boolean = False
    Private rightArm As Boolean = False
    Private leftLeg As Boolean = False
    Private rightLeg As Boolean = False
    Private checkPoint As Boolean = False
    Private answer As String = String.Empty

    Public Property TextWord() As String
        Get
            Return word
        End Get
        Set(ByVal value As String)
            word = value
        End Set
    End Property

    Public Function Guessed() As List(Of String)
        Return guessedlet
    End Function

    Public Function ShowRightLeg() As Boolean
        Return rightLeg
    End Function

    Public Function Difficulty()
        Do Until checkPoint = True
            Console.WriteLine("WELCOME TO THE HANG MAN GAME")
            Console.WriteLine("----------------------------")
            Console.WriteLine("-----CHOOSE A GAME MODE-----")
            Console.WriteLine("A - BABY MODE | B - BOSS MODE")
            answer = Console.ReadLine().ToUpper
            If answer <> "A" And answer <> "B" Then
                Console.WriteLine("Has to be A or B --> Press any key to try again: ")
                Console.ReadLine()
                Console.Clear()
            Else
                checkPoint = True
            End If
        Loop
        Console.Clear()
        Return answer
    End Function

    Public Sub Checker()
        If head = False Then
            head = True
        ElseIf body = False Then
            body = True
        ElseIf leftArm = False Then
            leftArm = True
        ElseIf rightArm = False Then
            rightArm = True
        ElseIf leftLeg = False Then
            leftLeg = True
        Else
            rightLeg = True
        End If
    End Sub

    Public Function Draw() As String
        Dim line1 As String
        Dim line2 As String
        Dim line3 As String

        If head = True Then
            line1 = " O"
        Else
            line1 = ""
        End If

        If body Then
            If leftArm Then
                If rightArm Then
                    line2 = "-|-"
                Else
                    line2 = "-|"
                End If
            Else
                line2 = " |"
            End If
        Else
            line2 = ""
        End If

        If leftLeg Then
            If rightLeg Then
                line3 = "/ \"
            Else
                line3 = "/ "
            End If
        Else
            line3 = ""
        End If

        Return String.Format("{1}{0}{2}{0}{3}", Environment.NewLine, line1, line2, line3)
    End Function

    Public Function WordAlter() As String


        If answer = "A" Then
            Dim txt As String = word
            For Each l As String In guessedlet
                txt = txt.Replace(l, "_")
            Next

            Dim returnable As String = String.Empty
            For i As Integer = 1 To word.Length - 2
                If txt.Substring(i, 1) <> "_" Then

                    returnable &= "_ "
                Else
                    returnable &= word.Substring(i, 1) & " "

                End If
            Next

            Return returnable
        Else
            Dim txt As String = word
            For Each l As String In guessedlet
                txt = txt.Replace(l, "_")
            Next

            Dim returnable As String = String.Empty
            For i As Integer = 0 To word.Length - 1
                If txt.Substring(i, 1) <> "_" Then

                    returnable &= "_ "
                Else
                    returnable &= word.Substring(i, 1) & " "

                End If
            Next

            Return returnable
        End If

    End Function



End Class