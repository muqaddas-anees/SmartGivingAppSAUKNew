<%@ Control Language="C#" AutoEventWireup="true" Inherits="WF_Controls_Footer" Codebehind="Footer.ascx.cs" %>
<!-- Main Footer -->
			<!-- Choose between footer styles: "footer-type-1" or "footer-type-2" -->
			<!-- Add class "sticky" to  always stick the footer to the end of page (if page contents is small) -->
			<!-- Or class "fixed" to  always fix the footer to the end of page -->
			<footer class="main-footer sticky footer-type-1">
				
				<div class="footer-inner">
				
					<!-- Add your copyright text here -->
					<div class="footer-text">
                       <label> &copy; </label> <label id="lblyear" runat="server"></label> <strong><label id="lblcopyrighttext" runat="server"></label></strong> 
						<%--&copy; 2014 --%>
						<%--<strong>Xenon</strong> 
						theme by <a href="http://laborator.co" target="_blank">Laborator</a>--%>
					</div>
					<!-- Go to Top Link, just add rel="go-top" to any link to add this functionality -->
					<div class="go-up">
					
						<a href="#" rel="go-top">
							<i class="fa-angle-up"></i>
						</a>
						
					</div>
					
				</div>
				
			</footer>

<!-- Document360 In-app Assistant Start --> 
<%--<script> (function (w,d,s,o,f,js,fjs) { w['JS-Widget']=o;w[o] = w[o] || function () { (w[o].q = w[o].q || []).push(arguments) }; js = d.createElement(s), fjs = d.getElementsByTagName(s)[0]; js.id = o; js.src = f; js.async = 1; fjs.parentNode.insertBefore(js, fjs); }(window, document, 'script', 'mw', 'https://cdn.document360.io/static/js/widget.js')); mw('init', { apiKey: 'RMi7oNbcPPwPaf3rGBsIFoOdL9dPmbIgPiqreaIovwlFLZiBKGJ5B337EJw0kXo7MFTSzCqc79BR4/8VgeMCDISSFBipOZhG38P05+4G8Ct9zkblAYZ13ScbG/XH51ro/HLy6lbh+jQYqh6+hq7awg==' }); </script> <!-- Document360 In-app Assistant End -->--%>

<%--<script type="text/javascript">
    (function (e, t, o, n, p, r, i) { e.visitorGlobalObjectAlias = n; e[e.visitorGlobalObjectAlias] = e[e.visitorGlobalObjectAlias] || function () { (e[e.visitorGlobalObjectAlias].q = e[e.visitorGlobalObjectAlias].q || []).push(arguments) }; e[e.visitorGlobalObjectAlias].l = (new Date).getTime(); r = t.createElement("script"); r.src = o; r.async = true; i = t.getElementsByTagName("script")[0]; i.parentNode.insertBefore(r, i) })(window, document, "https://diffuser-cdn.app-us1.com/diffuser/diffuser.js", "vgo");
    vgo('setAccount', '90208874');
    vgo('setTrackByDefault', true);

    vgo('process');
</script>

 <script>
     (function (w, d, s, o, f, js, fjs) {
         w['JS-Widget'] = o; w[o] = w[o] || function () { (w[o].q = w[o].q || []).push(arguments) };
         js = d.createElement(s), fjs = d.getElementsByTagName(s)[0];
         js.id = o; js.src = f; js.async = 1; fjs.parentNode.insertBefore(js, fjs);
     }(window, document, 'script', 'mw', 'https://cdn.document360.io/static/js/widget.js'));
     mw('init', { apiKey: 'RMi7oNbcPPwPaf3rGBsIFoOdL9dPmbIgPiqreaIovwlFLZiBKGJ5B337EJw0kXo7MFTSzCqc79BR4/8VgeMCDISSFBipOZhG38P05+4G8Ct9zkblAYZ13ScbG/XH51ro/HLy6lbh+jQYqh6+hq7awg==' });
    </script>--%>