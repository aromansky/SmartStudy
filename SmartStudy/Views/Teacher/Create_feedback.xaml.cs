using SmartStudy.ModelsDB;
using SmartStudy.Models;

namespace SmartStudy.Views.Teacher;
public partial class Create_feedback : ContentPage
{
    public Create_feedback()
	{
		InitializeComponent();
    }

    private async void Savebutton_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Title.Text))
            await DisplayAlert("������", "������� �������� �������", "��");
        else
        {
            group_settings g_s = new group_settings(Serializer.DeserializeUser().user_id, Title.Text, Description.Text);
            Client.CreateGroupSettings(g_s); // ��� ��� �������� �� feedbackSettings?
            await Shell.Current.GoToAsync("///feedback");
        }
    }

    private void AddUsersToFeedback(object sender, EventArgs e)
    {
        DisplayAlert("��������", "�� ������ ������ ��� ������ �� ��������", "��");
        //bool res = await DisplayAlert("�������� ������", "��������� ��������� ��������� � ������� � ���������� �������������?", "��", "���");
        //if(res)
        //{
        //    if (string.IsNullOrEmpty(Title.Text))
        //        await DisplayAlert("������", "������� �������� ������", "��");
        //    else
        //    {
        //        long user_id = Serializer.DeserializeUser().user_id;
        //        group_settings g_s = new group_settings(user_id, Title.Text, Description.Text);
        //        Client.CreateGroupSettings(g_s);
        //        // ���� ����� �������� ��� ��������� � �������� ���������� �������������
               
        //    }
        //}
    }
}