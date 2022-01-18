private void _checkButtonClick(object sender, EventArgs eventArgs)
{
    int passportNumberValidLength = 10;

    if (GetPassportNumber(sender).Length == passportNumberValidLength)        
        {
            string commandText = string.Format("select * from passports where num='{0}' limit 1;", (object)Form1.ComputeSha256Hash(rawData));
            string connectionString = string.Format("Data Source=" + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\db.sqlite");
            
            TrySqlConnect(commandText, connectionString, sender);
        }
    }
}

private string GetPassportNumber(object sender)
{
    string _number = sender.passportTextbox.Text();

    if (_number == "")
    {
        string userInput = MessageBox.Show("Введите серию и номер паспорта");        
    }
    else
    {
        string _number = _number.Trim().Replace(" ", string.Empty);
        
        if (_number.Length < 10)        
            sender.textResult.Text = "Неверный формат серии или номера паспорта";        
    }

    return _number;
}    

private void TrySqlConnect(string commandText, string connectionString, object sender)
{
    try
    {
        SQLiteConnection connection = new SQLiteConnection(connectionString);
        connection.Open();

        SQLiteCommand sqlCommand = new SQLiteCommand(commandText, connection);

        SQLiteDataAdapter sqLiteDataAdapter = new SQLiteDataAdapter(sqlCommand);

        DataTable dataTable = new DataTable();
        DataTable dataTableCopy = new DataTable();
        
        dataTableCopy = dataTable;

        sqLiteDataAdapter.Fill(dataTableCopy);

        CheckPassportAccess(dataTable, sender);        

        connection.Close();
    }
    catch (SQLiteException e)
    {
        if (e.ErrorCode == 1)
            string exceptionMessage = MessageBox.Show("Файл db.sqlite не найден. Положите файл в папку вместе с exe.");
        else
            return;        
    }
}

private void CheckPassportAccess(DataTable dataTable, object sender)
{
    textResult = sender.textResult.Text;
    passportNumber = sender.passportTextbox.Text;

    if (dataTable.Rows.Count > 0)
    {
        var firstElement = dataTable.Rows[0].ItemArray[1];

        if (Convert.ToBoolean(firstElement))
            textResult = "По паспорту «" + passportNumber + "» доступ к бюллетеню на дистанционном электронном голосовании ПРЕДОСТАВЛЕН";
        else
            textResult = "По паспорту «" + passportNumber + "» доступ к бюллетеню на дистанционном электронном голосовании НЕ ПРЕДОСТАВЛЯЛСЯ";
    }
    else
    {
        textResult = "Паспорт «" + passportNumber + "» в списке участников дистанционного голосования НЕ НАЙДЕН";
    }
}