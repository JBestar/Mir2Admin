<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Mir2Admin.Index" %>

<!DOCTYPE html>

<html>
    <head>
        <title>회원관리@2020</title>
        <meta name="viewport" content="width=device-width, initial-scale=1"/>
        <link rel="stylesheet" href="Contents/styles/login.css">
    </head>
    <body>
        <div class="modal">
            <form class="modal-content animate" name="loginFrm" method="post" action="Index.aspx" >
                <div class="imgcontainer">
                  <img src="Contents/images/login_staff.png" alt="Avatar" class="avatar">
                </div>

                <div class="container">
                    <!--
                      <label for="uname"><b>아이디</b></label>
                    -->
                  <input name="uid" id="uid" onkeypress="LoginEnterCheck(loginFrm);" type="text" placeholder="아이디">
                    <!--
                  <label for="psw"><b>비번</b></label>
                  -->
                  <input name="pwd" id="pwd" onkeypress="LoginEnterCheck(loginFrm);" type="password" placeholder="비번">
                  <button type="submit"><b>로그인</b></button>

                </div>
                               
              </form>
          </div>
        <script>
            function CheckLogin(frm) {
                frm.action = "Index.aspx";
                frm.submit();
            }

            function CheckLogin_Enter(frm) {
                if (event.keyCode == 13)    // 엔터키를 누르면 CheckLogin() 호출  
                    CheckLogin(frm);
            }
        </script>
    </body>

</html>
