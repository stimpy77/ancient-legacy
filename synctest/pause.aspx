<%@Page Language="C#" %>
<%
Response.Cache.SetCacheability(HttpCacheability.NoCache);
System.Threading.Thread.Sleep(int.Parse(Request["delay"]));
Response.Write("OK");
%>