Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration

Namespace GetItemService
    Public Class Utilities

        Public Shared Function CleanForSQL(ByVal input As String) As String
            Dim cleaned As String = input
            cleaned = cleaned.Replace(";", "")
            cleaned = cleaned.Replace(")", "")
            cleaned = cleaned.Replace("(", "")
            cleaned = cleaned.Replace("'", "''")
            cleaned = cleaned.Replace("\", " ")

            Dim ps = InStr(cleaned, "  ")
            Do While ps > 0
                cleaned = cleaned.Replace("  ", " ")
                ps = InStr(cleaned, "  ")
            Loop

            Return cleaned
        End Function

        Public Shared Function PreparePhraseForSQL(ByVal input As String, ByVal phraseType As String) As String
            ' These stopwords come from the SQL Server view sys.fulltext_system_stopwords
            ' Where the language_id = 1033 (English)
            Dim SQLstopwords() As String = { _
                "$", _
                "0", _
                "1", _
                "2", _
                "3", _
                "4", _
                "5", _
                "6", _
                "7", _
                "8", _
                "9", _
                "A", _
                "B", _
                "C", _
                "D", _
                "E", _
                "F", _
                "G", _
                "H", _
                "I", _
                "J", _
                "K", _
                "L", _
                "M", _
                "N", _
                "O", _
                "P", _
                "Q", _
                "R", _
                "S", _
                "T", _
                "U", _
                "V", _
                "W", _
                "X", _
                "Y", _
                "Z", _
                "about", _
                "after", _
                "all", _
                "also", _
                "an", _
                "and", _
                "another", _
                "any", _
                "are", _
                "as", _
                "at", _
                "be", _
                "because", _
                "been", _
                "before", _
                "being", _
                "between", _
                "both", _
                "but", _
                "by", _
                "came", _
                "can", _
                "come", _
                "could", _
                "did", _
                "do", _
                "does", _
                "each", _
                "else", _
                "for", _
                "from", _
                "get", _
                "got", _
                "had", _
                "has", _
                "have", _
                "he", _
                "her", _
                "here", _
                "him", _
                "himself", _
                "his", _
                "how", _
                "if", _
                "in", _
                "into", _
                "is", _
                "it", _
                "its", _
                "just", _
                "like", _
                "make", _
                "many", _
                "me", _
                "might", _
                "more", _
                "most", _
                "much", _
                "must", _
                "my", _
                "never", _
                "no", _
                "now", _
                "of", _
                "on", _
                "only", _
                "or", _
                "other", _
                "our", _
                "out", _
                "over", _
                "re", _
                "said", _
                "same", _
                "see", _
                "should", _
                "since", _
                "so", _
                "some", _
                "still", _
                "such", _
                "take", _
                "than", _
                "that", _
                "the", _
                "their", _
                "them", _
                "then", _
                "there", _
                "these", _
                "they", _
                "this", _
                "those", _
                "through", _
                "to", _
                "too", _
                "under", _
                "up", _
                "use", _
                "very", _
                "want", _
                "was", _
                "way", _
                "we", _
                "well", _
                "were", _
                "what", _
                "when", _
                "where", _
                "which", _
                "while", _
                "who", _
                "will", _
                "with", _
                "would", _
                "you", _
                "your"}

            Dim output As String = input
            If phraseType = "browse" Then
                ' This is a search phrase for a browse page.

                ' Get rid of any non-word characters and clean up spaces left over.
                output = Regex.Replace(output, "[^0-9a-zA-Z\-'*""]+", " ")
                output = Regex.Replace(output, "\s+", " ")
                output = output.Trim()
                output = output & "%"

            Else
                ' This is a search phrase for a normal keywords search.

                ' Get rid of any non-word characters and clean up spaces left over.
                output = Regex.Replace(output, "[^0-9a-zA-Z\-'*""]+", " ")
                output = Regex.Replace(output, "\s+", " ")
                output = output.Trim()

                ' Get an array of the unique words from the string.
                Dim rawArr As String() = output.Split(New [Char]() {" "c}).Distinct().ToArray

                ' consolidate quoted strings into single list items
                Dim quotedList As New List(Of String)
                Dim inQuotedString As Boolean = False
                Dim quotedString As String = ""
                For Each word In rawArr
                    ' Remove any quotes that are in the middle of words
                    If word.IndexOf("""") = 0 And word.LastIndexOf("""") = word.Length - 1 Then
                        word = """" & word.Replace("""", "") & """"
                    ElseIf word.IndexOf("""") = 0 Then
                        word = """" & word.Replace("""", "")
                    ElseIf word.LastIndexOf("""") = word.Length - 1 Then
                        word = word.Replace("""", "") & """"
                    End If

                    If inQuotedString = False Then
                        If word.IndexOf("""") = 0 And word.LastIndexOf("""") = word.Length - 1 Then
                            quotedList.Add(word.Replace("""", ""))
                        ElseIf word.IndexOf("""") = 0 Then
                            inQuotedString = True
                            quotedString = word
                        Else
                            quotedList.Add(word.Replace("""", ""))
                        End If
                    Else
                        quotedString = quotedString & " " & word
                        quotedList.Add(quotedString)
                        quotedString = ""
                        If word.LastIndexOf("""") = word.Length - 1 Then
                            inQuotedString = False
                        End If
                    End If
                Next
                ' Make sure that the last list item gets quote terminated if the input string 
                ' was missing the final quote.
                If quotedList(quotedList.Count - 1).IndexOf("""") = 0 And quotedList(quotedList.Count - 1).LastIndexOf("""") <> quotedList(quotedList.Count - 1).Length - 1 Then
                    quotedList(quotedList.Count - 1) = quotedList(quotedList.Count - 1) & """"
                End If

                ' Remove stop words from the array (unless they're in a quoted string) and
                ' prepare the strings for the T-SQL CONTAINSTABLE function
                Dim shortenedList As New List(Of String)
                For Each word In quotedList
                    If Array.IndexOf(SQLstopwords, word) = -1 Then
                        If word.IndexOf("""") = 0 And word.LastIndexOf("""") = word.Length - 1 Then
                            shortenedList.Add(word)
                        Else
                            shortenedList.Add("""" & word & "*""")
                        End If
                    End If
                    ' Take only the first 6 words from the array since SQL Server won't do a NEAR
                    ' searches with more than 6 words.
                    If shortenedList.Count = 6 Then
                        Exit For
                    End If
                Next

                ' Combine the phrases with the NEAR clause
                Dim shortenedArr = shortenedList.ToArray()
                output = String.Join(" NEAR ", shortenedArr)
            End If

            Return output
        End Function

        'Public Shared Function GetSubjectName(ByVal subjectID As String, ByVal dbConnectionString As String) As String
        '    Dim subject As String

        '    ' Get the connection string from the web.config file
        '    Dim connStringColl As ConnectionStringSettingsCollection = WebConfigurationManager.ConnectionStrings
        '    Dim connStringItem As ConnectionStringSettings = connStringColl.Item(dbConnectionString)
        '    Dim connectionString As String = connStringItem.ConnectionString

        '    Using connection As New SqlConnection(connectionString)

        '        ' Set up the SQL command to run
        '        Dim command As SqlCommand = New SqlCommand()
        '        command.Connection = connection
        '        command.CommandText = "GetSubjectByID"
        '        command.CommandType = CommandType.StoredProcedure

        '        Dim paramSubjectID As New SqlParameter("@subjectID", SqlDbType.Int)
        '        paramSubjectID.Value = subjectID
        '        command.Parameters.Add(paramSubjectID)

        '        ' Execute the stored procedure
        '        connection.Open()
        '        Dim reader As SqlDataReader = command.ExecuteReader()
        '        If reader.hasRows Then
        '            Do While reader.Read()
        '                subject = reader.Item("subjectText")
        '                Exit Do
        '            Loop
        '        End If
        '    End Using

        '    Return subject

        'End Function

        'Public Shared Function GetTypeName(ByVal typeID As String, ByVal dbConnectionString As String) As String
        '    Dim type As String

        '    ' Get the connection string from the web.config file
        '    Dim connStringColl As ConnectionStringSettingsCollection = WebConfigurationManager.ConnectionStrings
        '    Dim connStringItem As ConnectionStringSettings = connStringColl.Item(dbConnectionString)
        '    Dim connectionString As String = connStringItem.ConnectionString

        '    Using connection As New SqlConnection(connectionString)

        '        ' Set up the SQL command to run
        '        Dim command As SqlCommand = New SqlCommand()
        '        command.Connection = connection
        '        command.CommandText = "GetTypeByID"
        '        command.CommandType = CommandType.StoredProcedure

        '        Dim paramTypeID As New SqlParameter("@typeID", SqlDbType.Int)
        '        paramTypeID.Value = typeID
        '        command.Parameters.Add(paramTypeID)

        '        ' Execute the stored procedure
        '        connection.Open()
        '        Dim reader As SqlDataReader = command.ExecuteReader()
        '        If reader.hasRows Then
        '            Do While reader.Read()
        '                type = reader.Item("typeText")
        '                Exit Do
        '            Loop
        '        End If
        '    End Using

        '    Return type

        'End Function

        'Public Shared Function GetDateName(ByVal dateID As String, ByVal dbConnectionString As String) As String
        '    Dim dateName As String

        '    ' Get the connection string from the web.config file
        '    Dim connStringColl As ConnectionStringSettingsCollection = WebConfigurationManager.ConnectionStrings
        '    Dim connStringItem As ConnectionStringSettings = connStringColl.Item(dbConnectionString)
        '    Dim connectionString As String = connStringItem.ConnectionString

        '    Using connection As New SqlConnection(connectionString)

        '        ' Set up the SQL command to run
        '        Dim command As SqlCommand = New SqlCommand()
        '        command.Connection = connection
        '        command.CommandText = "GetDateByID"
        '        command.CommandType = CommandType.StoredProcedure

        '        Dim paramDateID As New SqlParameter("@dateID", SqlDbType.Int)
        '        paramDateID.Value = dateID
        '        command.Parameters.Add(paramDateID)

        '        ' Execute the stored procedure
        '        connection.Open()
        '        Dim reader As SqlDataReader = command.ExecuteReader()
        '        If reader.hasRows Then
        '            Do While reader.Read()
        '                dateName = reader.Item("dateText")
        '                Exit Do
        '            Loop
        '        End If
        '    End Using

        '    Return dateName

        'End Function

    End Class


End Namespace