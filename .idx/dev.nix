{ pkgs, ... }: {
  channel = "stable-23.11"; 
  packages = [
    pkgs.dotnet-sdk_8
  ];

  env = {};
  idx = {
    extensions = [
      "PKief.material-icon-theme"
      "k4ustu3h.theme-jamt"  
    ];
  };
}
