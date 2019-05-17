<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" Inherits="ContestDll.OnshowWorksSubmit" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
 <script type="text/javascript">
     function txtdiva() {
         document.getElementById("txtDiv").style.display = "";
         document.getElementById('btnSubmit').style.display = '';
         document.getElementById('inputbutton').style.display = 'none';

     }
     function YiComments() {
         document.getElementById('inputbutton').style.display = 'none';
     }
     function ValidComments() {
         var txt = document.getElementById('<%=txtComments.ClientID%>');
          if (txt.value.length == 0) {
              alert("评语不能为空！");
              txt.select();
              return false;
          }
          return true
      }
      function rate(obj, oEvent) {

          // 图片地址设置
          var imgSrc = 'images/star_gray.gif'; //没有填色的星星
          var imgSrc_2 = 'images/star_red.gif'; //打分后有颜色的星星

          if (obj.rateFlag) return;
          var e = oEvent || window.event;
          var target = e.target || e.srcElement;
          var imgArray = obj.getElementsByTagName("img");
          for (var i = 0; i < imgArray.length; i++) {
              imgArray[i]._num = i;
              imgArray[i].onclick = function () {
                  if (obj.rateFlag) return;
                  obj.rateFlag = true;
                  document.getElementById('HiddenField1').value = this._num + 1;

                  //    alert(this._num+1);  //this._num+1这个数字写入到数据库中,作为评分的依据
              };
          }
          if (target.tagName == "IMG") {
              for (var j = 0; j < imgArray.length; j++) {
                  if (j <= target._num) {
                      imgArray[j].src = imgSrc_2;
                  } else {
                      imgArray[j].src = imgSrc;
                  }
              }
          } else {
              for (var k = 0; k < imgArray.length; k++) {
                  imgArray[k].src = imgSrc;
              }
          }
      }

    </script>
    <style type="text/css">
        body{ margin:0 auto; text-align:center; background-color:#dddddd;position:relative;}
        .content{width:1002px;margin:0px auto; margin-bottom:10px; text-align:center; padding:0}
        #showDiv{ margin:0;
                  text-align:left; background-color:#fff; 
                  padding:10px 20px 5px;}
        .worktitle
           {width: 800px; height:25px; border:0; padding:0; margin:0;
            border-bottom:1px solid #ff6633;
            color: #4a4a4a; text-align:left; 
             vertical-align:middle; line-height:25px; overflow:hidden;
            }
        .h3css
        {
            font-family: 微软雅黑;
            color: #ff6633;
            font-size:20px; font-weight:bold;
        }
        .titlea{ width: 800px;height:22px;
            padding-left: 20px;
            color: #4a4a4a; text-align:left;}
        .titlebga{background:url("images/titlebga.gif") repeat-x bottom left;width: 800px;height:32px;
            padding:0; margin:0;
            
            color: #4a4a4a;}    
        .titleaspan{ font-weight:bold}    	
        .titlebg
        {
            text-align: center;
            height:32px;
            line-height:32px; vertical-align:middle; overflow:hidden;
            color: #fff; font-weight:bold; background:url("images/titlebg.gif") no-repeat bottom left;
            width: 96px;
        } .titlebgaa
        {
            text-align: center;
            height:32px;
            line-height:32px; vertical-align:middle; overflow:hidden;
            color: #fff; font-weight:bold;
            width: 96px;
        }
        .buttoncss
        {
            width: 120px;
            height: 40px;
            color: #777;
            font-family: 微软雅黑;
            font-size: 20px;
            border: 1px solid #ccc;
            cursor: pointer; text-align:center;
            line-height:40px; vertical-align:middle; overflow:hidden;
            -webkit-transition:background-color 0.5s linear,color 1s linear;
            -moz-transition:background-color 0.5s linear,color 1s linear;
            -0-transition:background-color 0.5s linear,color 1s linear;
            display:'';
        }
      
        .buttoncss:hover
        {
          background-color:#61a4da;
          color: #fff;  
        }
        .buttoncssa
        { background-color:#61a4da;
            width: 120px;
            height: 40px;
            color: #fff;
            font-family: 微软雅黑;
            font-size: 20px;
            cursor: pointer; text-align:center;
            line-height:40px; vertical-align:middle; overflow:hidden;
        }
        ul, ol
        {list-style: none;}
       .lblcss{padding:5;margin:5px;line-height:1.5;text-indent:20px; color:#555;}
       #floatposition{ height:223px; width:128px;position: absolute;position: fixed; top:15px; right:0; z-index:999;background:url(images/floatng.gif) no-repeat top left;}
     .f{ color:#FFFFFF; text-align:center; }
     .f ul{ margin:0; padding:0;text-align:left;margin-left:10px;margin-top:15px;}
     .f ul li{ padding:2px 0;}
      .f ul li a{ font-size:13px; font-family:'微软雅黑'; color:#b7b7b7; text-decoration:none;}
      .f ul li a:hover{ color:#ff6633}
        .grouptd
        {
            text-align: left;
            height: 25px;
            padding:0;
            margin:0;
        }
        .buttoncssb
        {
            color: #4141a4;
            width: 50px;
            height: 25px;
            background: #fff;
            font-size: 14px;
            cursor: pointer;
            text-decoration: underline;
            float: left;
        }
       .liuyan{font-weight:bold; font-size:13px;}
       .fenshu{color:#f00}
       .neirong{ color:#666}
        </style>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
 <div id="floatposition">
       <div class="f">
         <ul>
           <li><a href="#a01">作品简介</a></li>
           <li><a href="#a02">安装说明</a></li>
           <li><a href="#a03">作品效果图</a></li>
           <li><a href="#a04">设计思路</a></li>
           <li><a href="#a05">设计重点与难点</a></li>
           <li><a href="#a06">其他说明</a></li>
           <li><a href="#a07">作品点评</a></li>
           <li><a href="#a00" style="color:#ff6633;text-decoration:none;">回到顶部</a></li>
           
         </ul>
       </div>
      </div>
    <div class="content">
    <div id="showDiv">
 
        <table cellpadding="2" cellspacing="0" style="font-size: 14px; width:800px; text-align:left">
            <tr>
                <td class="worktitle" valign="bottom" id="a00">
                    <div class="h3css">作品：<asp:Label ID="lblWorksName" runat="server" Text="《愤怒的小鸟》炒热现场气氛"></asp:Label></div>
                </td>
            </tr>
            <tr>
                <td  height="10px">
                </td>
            </tr>
            <tr>
                 <td class="titlea"><span class="titleaspan"><asp:Label ID="Label1" runat="server" Text="作品编号："></asp:Label></span>&nbsp;<asp:Label ID="lblWorksCode" runat="server" Text="A120622"></asp:Label> </td></tr>
            <tr><td class="titlea">
                    <span  class="titleaspan"><asp:Label ID="Label2" runat="server" Text="作品组别："></asp:Label></span>&nbsp;<asp:Label ID="lblWorksType" runat="server"  Text="计算机动画、游戏"></asp:Label>
                </td>
            </tr>
             <tr><td class="titlea">
                    <span  class="titleaspan"><asp:Label ID="Label3" runat="server" Text="演示地址："></asp:Label></span>&nbsp;<span style="color:#ff6633"><asp:Label ID="lblDemoURL" runat="server"  Text="http://www.youku.asjkhdhk.com"></asp:Label></span>
                </td>
            </tr>
            <tr><td class="titlea"><div runat="server" id="divViewShow"><embed src="http://www.tudou.com/l/QACM1N6YOAI/&rpid=111905378&resourceId=111905378_05_05_99&iid=140062282/v.swf" type="application/x-shockwave-flash" allowscriptaccess="always" allowfullscreen="true" wmode="opaque" width="480" height="400"></embed>
            </div>
            </td></tr>
            <tr>
                <td  height="20px">
                </td>
            </tr>
         
            <tr>
                <td  class="titlea titlebga" id="a01">
                   <div class="titlebg" >作品简介</div>
                </td>
            </tr>
            <tr>
                <td>               
                <asp:Label ID="lblSubmitProfile" runat="server" CssClass="lblcss" Text="自去年下半年开始，我国土地价格并没出现明显的上涨，同时许多地方出现了土地流拍现象，地方财政不好过。针对土地价格下降，是否应该购房的问题，任志强表示，土地价格越是下降越需要买房。“因为房价下一轮一定是高涨，我们的规律是，下降趋势就意味着下一轮的高涨。”任志强说：“土地供应高的时候，价格就会下降。”自去年下半年开始，我国土地价格并没出现明显的上涨，同时许多地方出现了土地流拍现象，地方财政不好过。针对土地价格下降，是否应该购房的问题，任志强表示，土地价格越是下降越需要买房。“因为房价下一轮一定是高涨，我们的规律是，下降趋势就意味着下一轮的高涨。”任志强说：“土地供应高的时候，价格就会下降。” "></asp:Label> 
                </td>
            </tr>
            <tr>
                <td  height="10px">
                </td>
            </tr>
            <tr>
                <td  class="titlebga" id="a02">
                   <div class="titlebg">安装说明</div>
                </td>
            </tr>
            <tr>
                <td > <asp:Label ID="lblInstallationGuide" runat="server" CssClass="lblcss"  Text="但是，为何沈佳宜唯独愿意把心事与柯景腾分享呢？她对他究竟有没有别样的感觉呢？柯景腾暗恋沈佳宜八年最终能否修得正果呢？让我们走进《那些年，我们一起追的女孩》，一起来寻找那最纯真的感动吧！但是，为何沈佳宜唯独愿意把心事与柯景腾分享呢？她对他究竟有没有别样的感觉呢？柯景腾暗恋沈佳宜八年最终能否修得正果呢？让我们走进《那些年，我们一起追的女孩》，一起来寻找那最纯真的感动吧"></asp:Label></td>
            </tr>
            <tr>
                <td  height="10px">
                </td>
                </tr>
            <tr>
               <td class="titlebga" id="a03"><div class="titlebg">作品效果图</div></td>
               </tr>
            <tr>
               <td style=" padding:5px;color:#555"><div id="divWorksFile" runat="server"  >
               <img src="http://www.kw2007.com.cn/upload/Info/bfcecf60efd74f35b624b9b4ae73e59d/172300001.jpg"  width="400px"/><br />《那些年，我们一起追的女孩》
               <br />
               <img src="http://www.kw2007.com.cn/upload/Expo/4f67976ade8a4053a0aae8e8668cb89b/101900005.jpg" width="400px" /><br />
               柯景腾暗恋沈佳宜八年
               <br />
               <img src="http://www.webjx.com/files/allimg/090710/1152540.jpg" width="400px" /><br />
               让我们走进《那些年，我们一起追的女孩》
               <asp:Label ID="lblResult" runat="server"></asp:Label></div></td>
               </tr> 
             
            <tr><td  height="10px"></td></tr>    
           
            <tr>
                <td  class="titlebga" id="a04">
                    <div class="titlebg"> 设计思路</div>
                </td>
            </tr>
            <tr><td><div id="divDesignIdeas" class="lblcss" runat="server">柯景腾读国中时是一个成绩暴烂而且又调皮捣蛋的男生，老师将他“托付”给  图书封面班里最优秀的女生沈佳宜。只要他不认真学习，沈佳宜就会用圆珠笔戳他的衣服。在沈佳宜的监督和鼓励下，柯景腾的成绩就像芝麻开花节节高，渐渐地，他也喜欢上了气质优雅的沈佳宜。但是柯景腾却不敢向心爱的女生表白，因为几乎被所有男生喜欢的沈佳宜对追求她的男生一律有一种反感，她只想好好学习，不希望别人介入自己的生活。
　　但是，为何沈佳宜唯独愿意把心事与柯景腾分享呢？她对他究竟有没有别样的感觉呢？柯景腾暗恋沈佳宜八年最终能否修得正果呢？让我们走进《那些年，我们一起追的女孩》，一起来寻找那最纯真的感动吧
　　柯景腾读国中时是一个成绩暴烂而且又调皮捣蛋的男生，老师将他“托付”给  图书封面班里最优秀的女生沈佳宜。只要他不认真学习，沈佳宜就会用圆珠笔戳他的衣服。在沈佳宜的监督和鼓励下，柯景腾的成绩就像芝麻开花节节高，渐渐地，他也喜欢上了气质优雅的沈佳宜。但是柯景腾却不敢向心爱的女生表白，因为几乎被所有男生喜欢的沈佳宜对追求她的男生一律有一种反感，她只想好好学习，不希望别人介入自己的生活。
　　但是，为何沈佳宜唯独愿意把心事与柯景腾分享呢？她对他究竟有没有别样的感觉呢？柯景腾暗恋沈佳宜八年最终能否修得正果呢？让我们走进《那些年，我们一起追的女孩》，一起来寻找那最纯真的感动吧
　　柯景腾读国中时是一个成绩暴烂而且又调皮捣蛋的男生，老师将他“托付”给  图书封面班里最优秀的女生沈佳宜。只要他不认真学习，沈佳宜就会用圆珠笔戳他的衣服。在沈佳宜的监督和鼓励下，柯景腾的成绩就像芝麻开花节节高，渐渐地，他也喜欢上了气质优雅的沈佳宜。但是柯景腾却不敢向心爱的女生表白，因为几乎被所有男生喜欢的沈佳宜对追求她的男生一律有一种反感，她只想好好学习，不希望别人介入自己的生活。
　　但是，为何沈佳宜唯独愿意把心事与柯景腾分享呢？她对他究竟有没有别样的感觉呢？柯景腾暗恋沈佳宜八年最终能否修得正果呢？让我们走进《那些年，我们一起追的女孩》，一起来寻找那最纯真的感动吧
　　柯景腾读国中时是一个成绩暴烂而且又调皮捣蛋的男生，老师将他“托付”给  图书封面班里最优秀的女生沈佳宜。只要他不认真学习，沈佳宜就会用圆珠笔戳他的衣服。在沈佳宜的监督和鼓励下，柯景腾的成绩就像芝麻开花节节高，渐渐地，他也喜欢上了气质优雅的沈佳宜。但是柯景腾却不敢向心爱的女生表白，因为几乎被所有男生喜欢的沈佳宜对追求她的男生一律有一种反感，她只想好好学习，不希望别人介入自己的生活。
　　但是，为何沈佳宜唯独愿意把心事与柯景腾分享呢？她对他究竟有没有别样的感觉呢？柯景腾暗恋沈佳宜八年最终能否修得正果呢？让我们走进《那些年，我们一起追的女孩》，一起来寻找那最纯真的感动吧</div></td></tr>
            
            <tr><td  height="10px"></td></tr>
            
            <tr>
                <td class="titlebga" id="a05">
                 <div class="titlebg" style="font-size:13px">设计重点\难点</div>
                </td></tr>
            <tr>
                <td><div id="divKeyPoints"  runat="server" >柯景腾读国中时是一个成绩暴烂而且又调皮捣蛋的男生，老师将他“托付”给  图书封面班里最优秀的女生沈佳宜。只要他不认真学习，沈佳宜就会用圆珠笔戳他的衣服。在沈佳宜的监督和鼓励下，柯景腾的成绩就像芝麻开花节节高，渐渐地，他也喜欢上了气质优雅的沈佳宜。但是柯景腾却不敢向心爱的女生表白，因为几乎被所有男生喜欢的沈佳宜对追求她的男生一律有一种反感，她只想好好学习，不希望别人介入自己的生活。
　　但是，为何沈佳宜唯独愿意把心事与柯景腾分享呢？她对他究竟有没有别样的感觉呢？柯景腾暗恋沈佳宜八年最终能否修得正果呢？让我们走进《那些年，我们一起追的女孩》，一起来寻找那最纯真的感动吧
　　柯景腾读国中时是一个成绩暴烂而且又调皮捣蛋的男生，老师将他“托付”给  图书封面班里最优秀的女生沈佳宜。只要他不认真学习，沈佳宜就会用圆珠笔戳他的衣服。在沈佳宜的监督和鼓励下，柯景腾的成绩就像芝麻开花节节高，渐渐地，他也喜欢上了气质优雅的沈佳宜。但是柯景腾却不敢向心爱的女生表白，因为几乎被所有男生喜欢的沈佳宜对追求她的男生一律有一种反感，她只想好好学习，不希望别人介入自己的生活。</div>
                </td></tr>
           
            <tr><td  height="10px"></td></tr>
         
            <tr>
                <td class="titlebga" id="a06">
                    <div class="titlebg">其他说明</div>
                </td>
            </tr>
            <tr><td><asp:Label ID="lblComment" runat="server" CssClass="lblcss" Text="柯景腾读国中时是一个成绩暴烂而且又调皮捣蛋的男生，老师将他“托付”给  图书封面班里最优秀的女生沈佳宜。只要他不认真学习，沈佳宜就会用圆珠笔戳他的衣服。在沈佳宜的监督和鼓励下，柯景腾的成绩就像芝麻开花节节高，渐渐地，他也喜欢上了气质优雅的沈佳宜。但是柯景腾却不敢向心爱的女生表白，因为几乎被所有男生喜欢的沈佳宜对追求她的男生一律有一种反感，她只想好好学习，不希望别人介入自己的生活。"></asp:Label></td>
            </tr>
         
            <tr><td  height="10px"></td></tr>
            </table>
            <div style="display:none  " >
           <table cellpadding="2" cellspacing="0" style="font-size: 14px; width:800px; text-align:left">

            <tr><td class="titlebga" style="background-color:#6e5328" id="a07"> <div class="titlebgaa">作品点评</div></td></tr>
            <tr><td  height="8px"></td></tr>
             <tr><td style=" padding-left:20; height:20px; border-bottom:1px dotted #555">
                  <div>
                    <span  style="color:#777">当前评分：</span>
                    <span runat="server" id="divScore"><img src="images/star_red.gif" /><img src="images/star_red.gif" /><img src="images/star_gray.gif" /><img src="images/star_gray.gif" /><img src="images/star_gray.gif" /></span>&nbsp;
                    <span style="color:#1a66b3">已有<asp:Label  id="lblPersons" runat="server" Text="1" ></asp:Label>人评论</span></div></td></tr>
            <tr><td  height="5px"></td></tr>
            
            <tr><td style="padding-left:20px">
                 
      <asp:GridView ID="gvComments" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            BorderStyle="None"  CssClass="grouptd"  GridLines="None" 
            AlternatingRowStyle-HorizontalAlign="Left" HorizontalAlign="Left">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="点评者" >
                    <ItemStyle Width="100px" Wrap="False" CssClass="liuyan"/>
                </asp:BoundField>
                <asp:BoundField DataField="Comments" HeaderText="评论内容">
                    <ItemStyle Width="500px" CssClass="neirong"/>
                </asp:BoundField>
                <asp:BoundField DataField="Score" HeaderText="评分" >
                    <ItemStyle Width="60px" Wrap="False" CssClass="fenshu"/>
                </asp:BoundField>
                <asp:BoundField DataField="Created" DataFormatString="{0:yyyy-MM-dd}" 
                    HeaderText="点评时间">
                    <ItemStyle Width="100px" Wrap="False" />
                </asp:BoundField>
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <HeaderStyle BackColor="#494B4C" ForeColor="#E6E6E6" HorizontalAlign="Left" />

<AlternatingRowStyle HorizontalAlign="Left" BackColor="#CCFFFF"></AlternatingRowStyle>
        </asp:GridView>
                 
                </td></tr>
            <tr><td  height="5px"></td></tr>
            <tr><td style=" padding-left:20;color:#555">
                 <div id="txtDiv" style="display:none">
                   <div style="margin-bottom:5px"><span  style="color:#777">评分：</span>
                    <span onmouseover="rate(this,event)"  ><img src="images/star_gray.gif" /><img src="images/star_gray.gif" /><img src="images/star_gray.gif" /><img src="images/star_gray.gif" /><img src="images/star_gray.gif" /></span>
                    </div>
                   <div>评语：<br />
                       <asp:TextBox ID="txtComments" runat="server" Height="150px" Width="500px" 
                           TextMode="MultiLine" ></asp:TextBox></div>
                 </div>
                </td></tr>
                <tr><td  height="5px"></td></tr>
            <tr>
                <td style=" padding-left:20;">
                    <input type="button" value="我要点评" class="buttoncss" onclick="txtdiva()" id="inputbutton"/>
                    <asp:Button ID="btnSubmit"  OnClientClick="return ValidComments()" runat="server" Text="发表" CssClass="buttoncssa" style="display:none"  BorderStyle="None"/>
                    
                    <asp:HiddenField ID="HiddenField1" Value="0"  runat="server" />
                    
                 </td>
            </tr>
               <tr><td  height="30px"></td></tr>
        </table> 
          </div>
   </div>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
作品信息
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
作品信息
</asp:Content>
