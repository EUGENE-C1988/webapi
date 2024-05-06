namespace WebAPI.Model
{
    public class UserInfo
    {
        public string ID { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string JWTToken { get; set; }

        public UserInfo(string id, string jWTToken)
        {
            ID = id;
            JWTToken = jWTToken;
        }

        public void GetUserInfo(UserInfo userInfo)
        {
            userInfo.Email = "abcd@abcd.efg.tw";
            userInfo.UserName = "abcd";
        }
    }
}
