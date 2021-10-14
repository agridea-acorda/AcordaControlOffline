using System;
using System.Linq;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Pdf.Model;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Pdf.Shared;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Pdf
{
    public sealed class InspectionPdf : PdfDocumentBase
    {

        private const string PngImgMarker = "data:image/png;base64";
        private const string FocaaBase64 = "iVBORw0KGgoAAAANSUhEUgAAAW4AAAB0CAYAAAClmNjGAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAAEnQAABJ0Ad5mH3gAADFMSURBVHhe7d0HlGxF1TbggznnnHPOGRNXQRQDiBhBlIuBoCAsQVEMPyhRUREjKBcvxmvOWTGLOeecc87p+33qu5uvLE/PTPd0z/S5s9+1es306XOqdlXteveuXeFs9T//RpdIJBKJweBMm/8mEolEYiBI4k4kEomBIYk7kUgkBoYk7kQikRgYkrgTiURiYEjiTiQSiYEhiTuRSCQGhiTuRCKRGBiSuBOJRGJgSOJOJBKJgSGJO5FIJAaGJO5EIpEYGJK4E4lEYmBI4k4kEomBIYk7kUgkBoYk7kQikRgYkrgTiURiYEjiTiQSiYEhiTuRSCQGhiTuRCKRGBiSuBOJRGJgSOJOJBKJgSGJO5FIJAaGJO5EIpEYGLb6n39j8/+JRGJM6D4+//znP7u///3v3R//+MfuO9/5Tvf73/9+8x1dd77zna+7whWu0J33vOftznzmM5fPVltttfnXRGJ8JHEnEhPir3/9a/fLX/6y++EPf9h96lOf6j74wQ923/rWt7pf/epXhcQDZzvb2Qp5X+961+tucpObdLe85S27y172suVaEnhiEiRxJxJjgnf9s5/9rPvABz7QvelNb+pOP/307kc/+lEh8n/84x+b7/pvnOUsZ+nOda5zdde5znW6e9zjHt1OO+3UXf7yly/XE4lxkMS9RoF8fM561rOm1zcGfvvb33Yf/vCHu5e+9KXFw/7+979f6nEcqO+LXexi3e1vf/vugAMO6G584xuX8EkisVQkcQ8Mmquvyc50pv+cZ+b5IYiWEP7yl79073//+7tXvepV3TWucY3uQQ96UHf+859/869d969//at8ph2HDZmHaiTUyY9//OPulFNO6V7ykpd0X/va184gbHV/9rOfvbvIRS5SPGpl/M1vflPCKNphVBc75znP2d33vvftjjjiiO6Sl7zk5quJxOJI4h4QxE4Nyf0NYkUM/r/Wta7VXfCCFyz3mSB797vfXb7f7GY3K6QSQEBf+tKXugMPPLAQzQknnFD+AlXw2+c+97lu2223LV7hNEDGr3/960Xeq171qoPzLtUZot6wYUMhbW0Q3eY85zlPiV1vt9125a8JSOX8+c9/Xjzzt771rWWyUhot3HeZy1ymEPduu+32X8Y3kRiFM/+/f2Pz/4k5BqJ4+9vf3j360Y/uXve615VOj8A///nPd+94xzsKAVi5AEjm+OOP765//et3V7nKVf6DEDyHRE477bRy/c53vnPxEkGM9qSTTuq++93vdne84x1LGGUSBKnJC37xi190Rx11VPfRj360u/Wtb108zaFAWdTHkUceWUhbWaJ8F7rQhbr73Oc+3WMf+9gSrxa7vtKVrtRd8YpXLIb05je/eXeBC1yg+/KXv9z9+te/Ls+0YNQucYlLlAnLc5zjHJuvJhILI038QIAEt99+++LVIYwHPvCBxWt+/OMf361fv/4/SPYnP/lJd81rXrO7wQ1u0OvdBqG2+Nvf/lY8xZ133vkMMh8Xf/7zn4u3/853vrOEZUAoxmTcLrvs0p373Ocu14aCn/70p92LXvSi4jkLfwRpG8UIczzxiU8sK0V43iYZ1beP9jBiud/97leM4ygItwipGCUlEktFEveAgHCDiP3vw0vjrV33utct18ODu8td7lKWmwV42TzqP/3pT4Uk2qG775415Oc5+u6DfJExUvesT9zruvXK0ot4L4/0xBNPLDH0WBZH5tve9rbdNttsc0bYBgFK27M+0oM6T9fiHt9XGspsNPPCF76wrCIJqHcGdNddd+0ufvGLjzSEgNBNPgqh9MGzSD7DJIlxkDHuAQFpPvKRjyzhkWc+85llWA68Y2uFkeh73vOe7m1ve1v3zW9+s3jdj3rUowq5fOUrXykkJNYsRmsJ2+1ud7sS4zacF9t+4xvf2H3iE58oRLv33nt3V77ylUt44DOf+Ux3wxvesPvBD35QvHlpMgK8aukiVt4nr/oNb3hDCYswKOvWretucYtblPStwjBSEFbwXTjnta99bflL7q233rq7//3vX8jy5JNP7j7+8Y93d7jDHQr5kwnxGV1c+9rXXpAopwUGRL777bdfkaXuJmR52MMe1h100EHdhS984c1X+yGdd73rXd0+++xT1ni3UCeHHnpot//+++eywMSSkWZ+gDC0tn74BS94QfEGv/rVrxaP92Uve1n3+te/vtt3333LEB1hWGuMlA3pkcxhhx3WHXLIIYWUA34Xw7XKBIkg4pe//OWFIA33P/ShDxWiNtEpPs2LRji77757Sdc9GzduLKTOW+fxi/HuscceZcmbCUlhhi9+8YtFTjFjBM5AHH744SWEgqyf//znl1CK9Kx8kY/fELild3X4ZdYQkz711FOLzK1vc9GLXrTEr+sRzUJQzj5Sdl04y2gkSTsxDpK4BwjerMlIHjWC5MEayr/yla8sxPaNb3yj/PU7j+9973tfIXvEixRNnl360pcuabmPl87zFd/mqfvdShNhDfcZ7t/oRjcqxuCpT31qmWA06Slt+SIwpG1E4DnkjoA9y1ggeStcEKAPD54Xyxu/3OUu1+2www4lTiy8IiRiaZy8yWuCFXGTSbw5QiqzBONiJ6R6U6YayNYk8NWudrWJJm8ZQ/Wjbu5617t2j3nMY0oZE4lxkMQ9QCBK5CmOjfRsnxZS4NUiU0QprvqEJzyh3Gf9McKLOGodahCaQLoIXLp28j34wQ8uoQDp1PcyGJe61KVKesIhmzZtKgYhCNlnsTCGe/7whz+U8Ij/weQlg4K0yd8CQQoFrVQcmByW8hkZhIwBMjA2DOdSoD7UmTCSSeQ999yzbLo59thjy0fcfxIDkFjbSOLeQsDT5dVaM8xDFvbgGfLueLDiq4b9wiCIE2EjJZ4tEuI5/+53vytxc6GNWI/chy984Qvdi1/84nKfYT7vEaG538ewPyYyfWoy9jt5kDWvmyw+8uZ1961mIadP5DFrqMP3vve9xbi0IAPZR002tiCv0cnBBx/cHXPMMd3RRx9d5giEgNRfknZiEiRxDww8T+RmyF5D3NWaYsTMo3v6059eYt6f/OQnS0hCSOVZz3pWiUVbDx6beBA3r889nhGztvxNrNwKEfcgf/kGhEGEZ6Tzlre8pYRmeONIimwmEE2gmthEgDxYz3sO8SFoKzJMogqP+CBvuzilK89YXheI52dN3AyE8oj7t942kF8Zx5EDORu9aCMGVhnV+0oYocSWidyAMyAgEgTGm7YcDVkGkICYNu8O6fEIfTeJJvyBTD3r41mfiJEbypskQypIiQduiSHPGOnEZFzszHQfz1yeQhxi0bxHK0/8Fp4++VwXyvHdkkV5kg15i39/+9vfLkSNyG9605uW/JAjL166wjfy8Vf4R1p+nxUYGWu2GaS+sI06uOc971li/onEaiGXAyYSFcT7Lbm0qqaPuBkzSyiFOhKJ1UKGShKJCkI2o84WSSTmBUncicRmIGtLDq3OSSTmGUncicRmIG6ra3jdicQ8I4k7kdgM0z2WJdolmkjMM9YccdtGbYegTST1ewETqwvryqNdVos47Zh0Hou33CQS84w1Rdw8Ki92tV7Zsag2pNgx+NnPfvY/Tn8LIBDxTh15JbZar2V873vf657ylKeUc1asoV4tMCBx0mEiMa9YU8RtPbA1ydYee8uLzSo8LKfOOZsiYOPJa17zmnKIkw0rTuLzYoJPf/rTq+qlO0zKKXuMz7Rg3bLDqJwc6P/VgrXk1pDb/NK3YzGRSPwf1lyoBDlYixtA5q75gPMpnvzkJ5ddgc6ksAnEZhWk/YhHPKJszFgt8mZcnvvc55aDoCYFj7KWn/FipBxQJb67WrBBh1Ftd4QmEon/xpqfnLT12O5BR5EKmzgm1TnMzob25hJbwe92t7uVV4ZZdfC0pz2tvNl7NeANOLatT7prD2nbzv685z3vjO3cdgI6R8MpdYudLZ1IJOYDa4a4Db+9+8/BRobjAV7eAx7wgLJN28SY9znaIm77dXh/tljbMu5saR654z6FFWzUEI81qeav9HmzYuMOdWIAxNG9uMALB5zxLFZurbBQDVnc51qdnnNEyOicEb+LuSJdhsWW8Ii3O8BJuh/72MdKnD6OIJWWreTyd4RqnOAnxOJwKK8W80IFISFL3yKmG2SuDMrpeWeOxGSd60JLQjbKQF6jAEfCxoQiOY0IlI38kfco1O0izza+LG8ySEs958aYRGINnFWCNBCJA5cQhI+Dj5CWQ5mc8xwHGyEPYQPncrTvCRRS8YxYsFCLFxHwvnmwQgy2SDuYyRkfiJEBQNj+IkBEyatHlM7CkJfDlbwggMfvuvTE1BGv0IwYuxfsxqvEvGxAfowM79iLfZ2lbZv2m9/85nJOiZPrPvKRj5Qy+is95OfMZwTvrTPSF5pgBBgr+TIkzgJRTm/C8aIG9aZMCNrZJJ475ZRTitfPCDAODIG33pDJ+SLSj+Ne1YOzUZxX0ne+COOkXRgrxkDdqgcv3mU8XXvFK15RSPvVr351MUIMqsOmZgEGUXv4jDIQzlmhG9oskVgtbPEeNwIRFxbeQNReRTXq5a3ivQutKAiC53kiYWSEnJG/Q/Fvc5vbFK/wGc94RiH3hzzkIYUMvbnFwfsITGjCGdnOZva6LwTNg/d6MekhRAR473vfu5y3jbQQibw9iywZCobARKUT9Jzx7K3sDnXyHentuOOOJX8GRlxe+R3wJB2HSDkfWuxeXuFh8+gZGaEU93rdFhk2bNhQyBgJk5OHLR1l3m233QrxemONEUWQupcuWLkTcwctrNaJsJN6ePjDH15emBAHZ2k3q38YS3kwpurRCCS97sRaxxZP3DxKnhySQmiO1zTp2Ae/8dCRbx+B81wRN/JyWh2vFzFJ24lxjIKYuaWFPDPpIWyE7H/XeIs8XUTJu0SWSIqn7B7pOUXPcwgeWSJq162GifCNvJEu79jrxuRrZYbfySZteUCcwe1590nLKXvIV55x6h8C96ozMvHQlZMx4k0bIbgujzj1zymF4u2MlDwQKrl5xt5Az6P2PWSuIfxhBGAOgSxkCPlB6IfB0n48egZSXurJqCCRWMvYookbCRveixmLwS4UawWhBmTFq2tXbiAl4QRHpiKbFkIPCBP58A6FKhAOIvRSXkeeIkbhDqfP8ZgRUF8IIcB7dk8fUSE7E4p3v/vdS/jH4fzCJWT3v/c3hqFZjOjid2VEwOLU6g0QvXmAUSB/HMXKMJnEda42Y+MNPEIr0mvBO5eHuHxfu4h9k4cB2WuvvbojjjiijAQYjMXKM2sIqWhLH3IaNRgJtR/XR5UvMV2o4/isJlZKji2auHVwnqkPIjWEDw+3r3LdZ6JSh/MSgDhsCIGYhBMOEJIQZx0F5O2tMLxS5G9S04txETqiErPldQvbiJMG6fVBvtDnsYr/im8LMXgPJI+VYTEZqqy3utWtSkiIl60eIg9EjJyRevt2GoZCaAep8rAZO/cxfAxPeOY11KE0pI+sLaPcdttti4FiCL1ouM4jIISjHoRx4tVp8omRDoJmCMTYlSsmcMk2jU7BmBhZMXSMaHxCN0YBUYu3WzL6pCc9qewBsEzUq97aDwNtziSM4CQgp/ZSbmGj+qO+wtCuFtSV/OmS/qL+ahl9D13TxtNouwC9onPa0egy5koWa8NpI3TXhL3FAvqgDWWzbJctfnISgSIrBGGyTsWGh2QSTBgggCCRjfAEUkReJuYQtre1IGTvYwwCk550eL1eIAAaz4YdCoU0EZFGRei8dffzwr3+iyEhj8lHb6GRnvx23nnnYkSECcR1rSVHqJSSTOvWrStlQo68bSs9yMwYCJ9IR6za/X7ToSwl9JswDmJHhjqW8JF7KZ/3Vwpt+F++OoK0PCeOLqxCvtNOO63Eo92rU/L0Ea2QEU/f+xp1JiQnfu0lDa2XLEyj45molb+8lJd8RijKG+0gv9NPP73UlxFRGKNJoVMrv3i85Z8miU0iMxLqNwx2H3RGspqXoBfk0obCQn0fxsdqJCOypUCdIDl6RFe0r/yEjoSXkILvrptrEELyEQpTLnrBiM5qVCIPMjJG8uWcKD9dU6fkDBlDTuWgh/qdvsAIMcQchRhVTgL6pS+ZwFc38mJ8jYSEQ6U9a6gL7SAcSwYfMnBG6Kl+P4u2WBMvUqDUGhUh6fi8XKR19atfvVRsC14MJfOhYOKqGsGEZP36LCSmk/OckQwgH94YZXafPJGbuLXhPvKm5O6XJqISIxb6aNOjmO5FrsIVyJBMvFUGiVWnIGTyO/nkiTBcd01Hlr+yuk+n0aEotTzdIx0KaJLVdUZHh1Rvyo64GRIgn/SRtt945epWOuqWfDq0OvaMiVAy9EE+6kkdGZEgHN5SpK0d5KWOGEvpyycmMCcFlecJW7+uzmfVBdTB7rvvXrbyM3qjIH8GXnvzHBGfelB+JOR37RNy1t+1l3pTx4whx4P+qHvXpkUa8gpdRsT0Qx+iIyFLyBWI62QIY0Jeuq0f6BOckknbU/1wyMyp1PnSV04Z/Z415Ks/mWuizwFlZLC33nrrM7hhmlgzb8AJxaJA8X0xpdYoPu7rC1dE1UU67rV93GqPww47rJANYrPkzc5Eq1t417Uskb5Pmx74PWSGVm6/Q30P1On2pRH5x3eo0417Io0afelB3DdKpj5EPnFvmzYgNddaOSaF/DZt2lRCGbz4cUAOk7ujjFENHXaPPfYocxE87z4oL8JmQHirsbpHWeWDgBkxz0e98PoZYB+GNIbknkGMjJsQHV3zfJ/ujgPGg/FkUHiXCNs1+ZGLwWhlVMdI3ScI3jMBz5LR6qdRdbMYOFU8biOyOizBEERIc7llXwzKaTRhtFbrkvIZhRo9q59pI19dNkVQTCQt5iy0oOPohOLjQimWtVHwxOqCylvRYu29oXsAicY8Qd+qItB+QmMmTRcDj9tEtdUwowy/kEiEW3izriEeox+jJF4jYjNaqokb2fPKjUiQKWKtuzLDgjisTJJGPDsueLVko8NGUmFUjCCs7Tf6M3LkQdcykkXIJ8I+5hLIydioZ2lYcqouJyVusgj7Cc0h8YC09T3LVftG1NOEshh98LiNiGuok3vd614zmVBP4p4iVKVQgY0z1l+ztJbMidfamDMLy5uYDMgPqeh4AaMja8etw6+9wxrCWscdd1whnMWgsyLvPtKWr3CWWLlQU3iwDEOEEPzv+VGgb8iLzonzIpDa8+R9MwCG7GK+45C3tBkGMX/GrDYq5oV4s0J4YveLpaus6ppnykghceXVN3ikkxB39DWEydP1vYbRrj0I8pil1y3Wrw1NwtfGA+RrfseRGUsZoY2DfMv7FKHjsfDeYG5lhQ5jdQfrb9ibmB/oVEgI8cQHSSLAxXZO6ogmYt2/0Cfizy2kbS7E5GaQNiBq+hJv1F+McMIwuJdBkQ6vL0YL8kG4Yq9CJmRfiueHBKUjBCF+LMwB6gsRmTzmxS81hu4eYSNykoM8RjrqR7hkqRO3NZTNRKD4svTVc91m6oDHS051NCsoB4/fCEg+rd74zmhOe6Q92fgpMRKUCHnzRngmQiSztPiJ4QGZ8tCEIIK0kRhjIAQzrgeKtIQrOAwmg2tjgcBMJoqfI+DWM+2D+6z0iYljQEocECNHMfRJdNoz5LO0VBnVQ+ulLhVCOCbulc+ErLBNXW7pmnRHqEsp8ySQh7pl5NQ/o1Z71vJlpMgxaTlHIYl7AmgEnpJlZM7jiM63pUOHNiw0sSe2mhgfQhl0R7y3DmtYEYTQJo3JchiszBHTbkNy8hHukO9iBEKX3cfTFt4AaSNc4RuOiO+TArlKywhhOUCGQjlGsgwe8m5JU2y9L4wyLTAewj68ao6a9uNZ1/Uj/GbiuV5xMg3MHXGrZIU1BKHcFMi6yGkXfBzIm3UPRSYjpfFyBTsDp21NwWoBQ7BpKR7PhKLFMHoSIACTVFbM+JsYD9pRXBuJhi4BwjFRt9CSwaWARys0IK02PKDtI1Y9CuQzyWl+hqcaECKRZiw3XS5MYlqyJ5QxSXriyghTHfJ0jWwZrTYUpF/ikFn0T3Ul3GXClvFA3MJAZGlHPAyIlTXT6MeBuSJulsvwzOSQ7dI8O6fNWU7l7AuNtdIwVORZO5/biXk8kojNidvVHXCa0MnsynvOc55TDNlyIT1bx20UmJS8Y+KMIVsro4xpAoHwvnT46MSIRoc31J/Gel+k2GcEgpRNEI5qf7ouZoxoAuTjHVtbP61YsXRMbtqcJe49DoIw1SPZEKYy8+L7wkTKyzufNtSVkYm+YIRjjwECt4KkjWdzQoVU6hHWcjFXxM2LQ9LI8JBDDilk7ewLJE55pkGSKjqWXi0FKtwkiHWhJpN4TMALmVXsmsJRCh3IDkZWfRqgUDrNJF4O6BQrPck6bnvNM4SaEGNt9NSpmDECm7RdakiDZxwbmmrwVIW4+rbgI0S6ra5rgmFMeJHjEuxCIKOQEDk5QeOAE2Otu2WF0ogdkuRkXFrjZ+Rgtc00nJ8aPGjGA4we1I/67tvZqz+rdzJPy+ueG+Km1LYg66iOI2U9WdKIYTlDRKUsByrQgv3HPe5xhRgXAw9JA1kdwvsVP5z2kKcPwiPqwe4vhCWOvlyIv1nqZrVL26HnFdrLygZ1b8g7dDDArf4wpDr6uBOSC0GfQSYtiRnRmkijWy38hhB5h7V8HBRO07S87eUCT/CgGZeIlSNJHyTexuD1YV630OO0oH44dOoRR4mvqyf5co4YutYgCa3q13R6Gpib5YBi2c6NsBQKYbUFN5xUSSpNJcTLCnjpFJXXwrqaPHO+hZgeT9nWZp1eI2s8BwTxYllrFS9dZ2Lb+cSjN/suJKIjRZpCNA5PYrU1ko6mAWzgEJ90tjXFRrLyl6c0eRTieCyz9J3pIX3Pj/JgdCBL0ljnXXbZpZzlQe44q1r5fZeP0wets1Uf4pKe4aXbBkxZrXMli/sRhuvhFQAPSz0KR9mQwkujeMosvu5Zf+UZHpx0hY622267YlB1IF6aa+qV/OradSsnlFt4hgzir33hAOlri6g7YR11RI6YANYevvcR0rSgk8/qRQrSU8dIp06bftigs1ynpAYC0Va8vHaUqq6FF+h2TXB0W3sJpwT8rh2slph00nSaUG/6onokm3BLvT7dXzpCR6OOlVdf1ReVOe5dDhg369EZQYbDaaH6JsQoHLHXdU8eHGFdfT2JOinmxvVijVhTRForVAtey1FHHVU8Z7E8nrOty74jaxV65JFHlklD9yJNZ4cgeb/7BBCACkWmvFFb0hGh2HocfOQ6ZRbT1qGdCGeytIXGdCYFAqXkscUdqTkdUFo8dgRl6DYKCM7OPZ1Lp/GMZxkCUEcnnnhid+qpp5Zda2Rje+VDeRCDuLh5AgdE2aCAsBk48iiXMiuDekHuFJpRcZ/44bOf/exSB+KujJFwFYPXTvL47jn5ifMhCmkylIyKSUwdRjspg7L1gbHxUgUyMhyODLCJSceI9qIT2msaHW81oN0YzyCUgM6MvKcNOsjRaSFMQ1dbORj9diivzulUkNJqQ1w5Jnb1DX2kdvDUZd+a6fDSpxEuofNGJvqJ/OIMoBr6Ewej5jH13TfimhRz0ws0gIJSnlapAiwnrwwh8Xp8hFVYWJ6ajs07YPV4qN7Asu+++xYLh1Q0tp1USAYhCoGoZBNuGsHpf+53GJRNBsifhTcxafus9IwMeKg1NATP0vZbJMUb40HFQTzIHDkhHbu5NOooMDQ8bt6w9CgFo8bzpXiIWlkpzJ3udKey88w1k1HCIbFGVpmci40899577+Idq2OesHQ2bn6lmdGCz/r168sOT/XE6HiBgo0mztqQnq38baxdvTMiiIcRUbcME3mRt/JrT7FHIZq++LiO4HwXIRHeiBENj11HU6ZoL/Jok0k2a8wDkI16bzut8tDBaYPO0/d25Kq+eaVtH6MLdWwb9EckqP3nAfTPh1x9BK1/8YDpT23g8UbExZdLmtpRWv7SS6PIdq5Lm9Lj1iCrY8+2DtAkmBvi5t1REF4XxeoDb4HFBZ45pTSEtzEA4SEN5K1h/aW8GlfcS+O5HorsL7J2zV8N7a97kT5y5c1rFA3kfutY3d/GW0MuVtiHFyuUgDiRFsI3EmAUnPMtjT7oOO4jg1AKmdULMhb+QYjKxZuSh3oy0aSMDEWUicz+dx+yQ+K+hzILrfDQpUXJEKRT7AzZ1aM05e8Znd91Q7/WY+a1I2dyMHAIyDnUDJfJXJs1jH4OOuig4nFLr4UOEKfhMUAMFpnJQwZliXZTtqFCncZmlhZ99bJcqDOecqtrdLWPwFzr80jVf0tMqwF9g67xnvEE4u4LOXB0eOKtrtAtzy+HNNWZPoB8/Y+0+9a162eMh35V/6YMlhWP4rdxMDfEzULxgBGAlw20So7EQGdmuZBYgGIhjYVWPKhMleij0nkc/rYKHHA/wjasNGQHz+gMbTxS/ghQwwipeF+kc7t5meTlFTuj2+vNlE2Yow+Uy2jCs5ZKScfBVA6qEc7xYbCQGiW0AkdIw8QtQ7FUUGpKLz15KlfUByNAsdRv1I86Q+Bt/cYQ0XVnd/DOjU6ER3Se/fffvxy45b446rZF1J2QiLpzprjyI2/pyruWb1R7zTsYKDq9UvIzBohjqaTLsNSrXeYNRq6cBzyg//GswxGpodycLv2khuc4JaOM51KA9I1IcQKnh1M1inP0o3a7vbb3bKxMWw7mhrgRyUMf+tDipYlvHnvssSWuy7ohc+EGhI0MDUHEbnlqPF3XeckUtYXK0mgUWENrUFZb2jxnFRmdqbaO7o9JR28y1+gm2Xj4Qgg13OuMCWkjKKEGsWThDWEPyxkpjNCGkEKfh0UGXjUvWRgkQEFsZaYEXuYQhkQIAcl5Ka9JzHHipOpJPesM5DWpGPUhbx6+yVjxfp6y68JSEeKJerJmlTevLRC0OQDPaBcTzQyU0JG2YcDq+g0gbRPSynnMMcecMb9gAkq7qlOGJOSbxnB3NUDmlZRbXetTfXU+NCBMjgq913eQ9kLhG8TOcaiNlrpHmEaqnIBJQB/pNgNHn8kxyjCqe6PtNv7NQeF1L8eAwFwdMqUyIhyh41o3rRMLnygwokB8YqGu8xg1hLixGLTOr2PHcBvZ8SRYSbFvhIQYxWY9SxGk5TnpIDOef1hyy3ooCPJ1v4bj3TocHTSA58XLI9QjfMCYiNFSINcRDgJ0zVkU0mjjcxRL/B6JIbvam5CuMJA6ELZA8OLs3qKjnoRXGCPySoehE65Q5rD4ZBdnZwSQPqVSz7xg8jIIhp+ek7/rSFha2oSRUHfkYMSMjqSjTdQ3WdSHNF2Xr3bzPLl54spWeyDgfgZBfeicVgJpC167tmBk6vZSn33D02kAQcxqVYmRDT1owxHKPerY1+VCe2uTNjxg9GR0U+fJm9WudbnpoHbQxrOQb6lguOmiMCQdtJqEXKN0wHUjHOWp4/YcuAiljBue0q+Qto90OCyh530ggzzotL7XInR+Uj2e22NdNZa4J4XRUXlmNVgsZMXTrGe9KanOUQ+z3Yuco7H87ppnpa8hXGMlWwVVPZQgYsl1Q7nue1xzL5lYZPdGWmRCeO6TZ19jRT7K0tdJwkJL56STTir3MnT+BoHzcHn+yiKvWjmjjNIPo6BuKJX8EFItlzLIy/211yAdsmiPSEfncG/bFuoMaUtb3S4GaagrIaracMmP7NFes4J8rFjyGRU24GWdcMIJZZQzDrSRkYky1tCBhb7oyzRBnxg7q3u0Q0AbIx3y6xMBE+E+dbm1gRGYESZdWw0oB4Nn8l98GekKy9GFhYA7LKXlLIUxUnaxZ2Vvw52LAR9ZQaUdcYtRJOKu9bQFXRYp4GjpNwHPcyw5RHUbjIO5PdZVR9eBW7IMICUV0FpOFakygoT89XxdwZ6pCczf9p6A56Unr5Y0PFNfcy+5a1ID/7vmt5CrReTTJwOQWV4UZ8OGDWXiUyjHKIFhE1KKSdW+svjuep2//0fJJS9lbhVLOnX9QtzbtoX8+q6PQtRdK0vbXrOCjjYrj9tooc/jVmajNro+bfR53NrEqNVoptZdI1Ved+2hagfl5XG3Q/6VAqNjJZdRMxInP+fISG6hj/4gFMixqRFhDgTe6tkoyJf3joQ5IvQREZsH6ss7PurTSIscNTwrDUaoT9+Xgtn2hMTUwdOgjIcffnhZv239s3i0icFxySSxcjDER35tJ40RzLSBbJCwvzWQNR1qjSAHqTXSnuVptqOElYL8ec4RwlF3vG6hPaOJUR/hNgaL7G19q2vptYS+EBhbJCy9+M6Q9OUdHzJysnj86ryWQ1mQvrK17bNUJHEPCBpf/BlZW9fsu7i0JXhi/Kx3Yj6hbYwaWiJBIDzjSTvwKPCyeXv1EB14em1YDMwlGB3VIBP5EEybzkpAnjxn5QArx4wWzHOM+hgdmNsRhjCn0q7pVibpmZdZap2b/+FBM4Ta0QhpITn8xokyn0QO/7d9kxFQtno0NA7y1WWJxGbMMsat09u+b3KrDsMY+ptsE+aq5weWCyEGE9gmxWvSJb/lqSbua5DP7mKeYn0/b9GkvgUA05RvKeDp22wmxGS0YkLcpPpCITO/MUDqVTmsRhLnr9vTyMKCAhvuhKoWgueESIxq1ZH8tdVCThKjKN2oL0bCMRq89ppuTbCatG+XLi4F6XHPOXRyCjOKSBLDACLhMbbxfuQiBipuO00Yzgsr1CSM1JBE38QeucS9W0LyvFU9RgUrCQQnXx9Qdzxd8otRj/rUc2LKa0TKWNXQl3i76mcxv5VnbGWI+pQu4ka4fXnHx3yFekTgPmTuO3jKwgBpj5pPWQhJ3HMCCtQOm1yzNG7jxo1lQrKd5EgMBzow4mm9Vp1WKMLk5SQdeBToiom0GjxNE2JCJS2QnJUWSKcNo0hLzLidWJ0ljH6MFvwlmxHCuCtvlMPcgtBFTZr6lXCJz0J17jdb7GMzmrS0IXnGgXonQ1vvHDJeeNtOS0ES95zAJIYlT6EkQHEol2VkDnniIfAWTIxQqLhvXsGj0PkmUcwtEdZP8/7ajq9drf+f1iRl6EhMpgXEsRFPvZqkht95huGxBjgU1vXTzWkal1Gg18ILkR/v1WqpSUI1yqLOW9KkkxG3HoWYlBSyCe+9z+gtBdreqKFue2VTTn183L6cxD0nEF/cb7/9ys7BCIvoYIZm1ou6Fgp96KGHlq3zCyndaoOsdpralm+WP/G/63etoW6X1glHOBeHMV4uMap33ns78cXjNLGNPFqPOiAuSz6hgBbIxSaYldi5Sq+FEBg0MMGINEfJvRD0IaTfvlhCPTOWSHlUeYyEGEDtI7xkTfukcX7GxyRl/bx81eckk5RJ3HMACsLb0qi863qnFWWtFZbFNzkSpyCuJHhwTvGjzIuBzGb4t99++/+KMa5VaC8EZNjfet0IxESatl8OMSI9y92ENyIdbSFPxN160zXIhLTpVhsHR14mCe0inuXywCAzI1BlEWagP31x+aVCiEO52v5iXsFIos9YKi89D+MhTt16zOMg6la4p+7PysirV+ZxkMS9yqCovC3ejm3ltvU6aGoUKLCdY45gpYgsNUI1rPOhADoWZeSlUzxk0Fp0CuNev8dvnves5xgSy6AoNxl97M60flzntUwsFN798mCA6l16PBSbhHQ8z/st0veX3K4DGaRBJrIF3OuatOUZ9w8ROqzJK16tvzXUJaKw6qSuw3GAbBCeDSsRnpKnEIiVK4brNWn0AVHyDB0D3K64UP+OMbBBiWHoI7zlQpomJCNMIuYuPrwcJyUmXttNTnRLuKTPENE5hso96kQIqSXdceA5G+WkU5eFPhtFjzvamtudk2sFOgMvljXfZpttyvInJOYwLQqHTL0kgiJbq21Y5c07CNUQEsk7K9vvPC2xcGeXUDgGgQfvECnDc0NGCmQobRuuLc7+usYrseTJ/Tql2LTDsmwi8BwIfTjoypBf+oiAHOSTji3BiINy+l163l7DW6GUZHMNQVuiRc54iw4yIA9ZdSSTaMjHyx2cWcNoSFsH1JFmAfU+q52TAZ4X0lZmKzXUU8A1Bgph6uTtKoSFQF46oC3oSMjP0FtP7IychZaw1TDyow+MOgKr64K89IPRld44O2NbIC36XT+vPwjJmJRHcEjbCKA1IuOCnMiR7ofx91fZOBb1+Teu8cTpPvnUhTXhJm8nJW5QHsaVsZBugN6Rr29+YRTS415l8LDF2njQlNRfXo1Z/BY6DXLeuHFjITOdx7OOjEVwlF4n8HID7+8Ul0Q0zmzwVhtKy4t21gnSpCgI2sl+vD0dxsmMDruiYJTIc+LpFC0mz1ynaBRf50Y2Nh7o8Mcff3wxRGRBtmSlqGRF7FbH6BC8KjJ4dtOmTcVgGEoyBNZRW0/McBx99NHFQCEfsk7qjc4TkJB2tj5aPQbUp/ZhOByittSwCb1g1CKMpe3CGMsH6YwbakBSHAnhrpbwta32sTaZrjAUdEO+C0FZyOp5zxi5CQ/FiM/vdFR82//0jNNQ19Gk0C/MF7UGgHNAlppI6SqdJad65KAsZbSyGBhtRqI9J4Wh0B+0/VLaG5K4VxEUFnnxkmzqMPihuMjY+/9qTwd4moa8JlooEVK2g1KndGaJw4rWr19fDACyc/63lzjw3nljSFZH4fnq1Dw8z1JaHrlOzht0treNAXZk+h0pUDhemzzdx+ukzDxg+ejk4tnkQh7ud+pddBQE4EQ63rfzxZX1uOOOK0TMWLjPszqpzs3T43lTZvWDPJyq2IYYhgrDdgeC2VmnToIUdFxlZpiNTnh+2k2d0If40B3tpl0dH8xw0xvXg/DMhdhoos3GBZKRhhdieKeicEtNXEjaEJ+emlg3gmKoGVdE6Lf48HTphBCOkZnVUzYHGV2ZuObBA9ndF2EedbTQKphxoDz6Tes1h9Gjb6BuyUNmZaSX5iX0l2lAW/SFfuSv3sKILYYk7lUEYuJ1HHjggYUk99lnn/J+SsNwHbdv0wOlqxUP6ms6bb3e1XcdACFQCh67fBEyD0Te3kWJWKFOC4lS3FFegOtIg8zedMOj5q2E59UnZ/zVieRPFgqLmIVHvEDCW4IYJAZBPJ9Xt9dee5XTD3mhq4m6fpYDaQR5I9gYsSAY9aoejUwQstGIkBMvHPn5IEwer9/oCgJHOgiGgUS4Yunjeto1kAvjHDLGOnQyBhAfcuaBewUdUkbi/sYnviuL0B5jpHzCE1ZMhUdt9EU/Y8SAMBmMaUHd0DF9IqCujUyj/vQR3i8i9Rui9Uxd5uWA58+pYqxrMMK8/DBaiyGJexWhI2ooHi7PlSXmpep0CFG4hDKNA8rmE+QS3ymej1h6TBIiCzFmeY+KU8azoCP7zlMHYQ1hFh7iwQcfXDxpnWOpSk5GxKIj6bTIHOlY/aAzk9NbgIR2xDm9RJhHNm6djAPEVHds9aJMSJYHasKVnNNAlN+r93baaacyaqEDRhXkQGC8P0RHH4S8kLWP+QAEjjS1C9nUkSNYbU9Hsm1YYBKQ0WiMEdhxxx27devWFSOPeBjb0BuyCi3QK+E/xtiHN0tGbalehcOUd4cddihGmjePHD0fni+51Tk9mEYZAjHJGPXrE86JfJFnhEmi3O6fRpgkIB190CofeZNBPdID9aee9LHFkJOTqwSeivgxoqa8QXaUCzHxUnjOiNUQlEegU+oohqZCEc701VG8oUeoRCwzJv50LCES6Qo18CJ4rxRGuMREo2uU1BAbMcnDvdLlCYlbnnzyyYUEhEYoVWwEomR+RyDSofxWRPColMk5EL5HegjPvfJyzgdvC3RmHZvMZNF5PSc95TLxKjQjTwSlDuqJpGlDutqGl4U4xNa9ucgIANFYJeNaO9RdDrQRktA26hq5aT+EpnPLSwdXJ/RDR0cmdIDRpSNCaOrdNfcs1XguFdJDLuqFwec1agd6Q06ykVE5yOwaA0ce9zPIQm3i+trT88qhbOqcztMpz6kH+hce/rQgH2WQL/JUFqM+cslL3uSg5+RzzehXW0yzPuk8OfRvBlf9MGby0q614zAKecjUKoFlt4KEB0OBavA+/aZhDVHFwZEbr4zymczjCSBEnoIJPwfw6LyI22FGOo1rYNKKF+R5ysmDM2wViqG4FIbiIE33ImleldEAb9fv4te+G/JarYLU5CcNQ2SdGImIWeqY8na8pfR4gDxJMU3Eveuuu54Rq9ZRrCAQZuFNGx7r5Dw8ZUb2QQiuIalpkmYLXpcRRMQaGUpEpOMGic4a6sSoxodxNLKpQQb1gaB1cn9XQq4WISNDRzdiJAbkQU7qr5ZxIYMrnQizudezC90/CdCdtq3zqYnS73QA5O23aRtBkD855Cd9ZR0nnyTuVYKG0ymRQaucmoTVdx1p6RA6c3gfPBMNzctxXafxGyXzbP277/Xz0ox7dBT38EDAd0obnpD7kEZ4eiAtz7rHtbjH/dKPIbFn2vRi5UFbZmm4z++ejd9DTnlKO+Rfa1APLeatHoYg45aEJO5EIpEYGKY/BkgkEonETJEedyKxAhAiEq6aBwhbzSJum1g5JHEnEjOGSSgTwlbL1BN4qwFxZ6sYbPwxD5IYJpK4E4kZw4StVULWoVtNtJrgbdsncMABB5TJ3sQwkcSdSMwYVstY7mgNfLu0rw+8YqtrZuERC5FYk77nnnuWlT+JYSKJO5GYMcS2bav2ifXDi2Hcdb1LBaNg04xNPhnnHi6SuBOJRGJgSJObSCQSA0MSdyKRSAwMSdyJRCIxMCRxJxKJxMCQxJ1IJBIDQxJ3IpFIDAxJ3IlEIjEwJHEnEonEwJDEnUgkEgNDEncikUgMDEnciUQiMTAkcScSicTAkMSdSCQSg0LX/X+WHOhculshvQAAAABJRU5ErkJggg==";

        private readonly Phrase OutcomeKey = new Phrase
        {
            new Chunk("Validation exigences: ", Fonts.Helvetica8BlackItalic),
            new Chunk(" Oui ", Fonts.Helvetica8Black).SetBackground(Colors.Ok),
            new Chunk(": ", Fonts.Helvetica8Black),
            new Chunk("respectées", Fonts.Helvetica8BlackBoldItalic),
            new Chunk("   ", Fonts.Helvetica8Black),
            new Chunk(" Non ", Fonts.Helvetica8Black).SetBackground(Colors.Ko),
            new Chunk(": ", Fonts.Helvetica8Black),
            new Chunk("non respectées", Fonts.Helvetica8BlackBoldItalic),
            new Chunk("   ", Fonts.Helvetica8Black),
            new Chunk(" P ", Fonts.Helvetica8Black).SetBackground(Colors.Pok),
            new Chunk(": ", Fonts.Helvetica8Black),
            new Chunk("partiellement respectées", Fonts.Helvetica8BlackBoldItalic),
            new Chunk("   ", Fonts.Helvetica8Black),
            new Chunk(" NA ", Fonts.Helvetica8Black).SetBackground(Colors.LightGray),
            new Chunk(": ", Fonts.Helvetica8Black),
            new Chunk("non applicables", Fonts.Helvetica8BlackBoldItalic),
            new Chunk("   ", Fonts.Helvetica8Black),
            new Chunk(" NC ", Fonts.Helvetica8Black).SetBackground(Colors.LightGray),
            new Chunk(": ", Fonts.Helvetica8Black),
            new Chunk("non contrôlées", Fonts.Helvetica8BlackBoldItalic),
            new Chunk("   ", Fonts.Helvetica8Black),
        };

        private readonly Phrase ColorsKey = new Phrase
        {
            new Chunk("Traitement des rubriques: ", Fonts.Helvetica8BlackItalic),
            new Chunk("     ", Fonts.Helvetica8Black).SetBackground(Colors.AutoSetNa),
            new Chunk(" ", Fonts.Helvetica8Black),
            new Chunk("non inscrit ou non concerné", Fonts.Helvetica8BlackBoldItalic),
            new Chunk("     ", Fonts.Helvetica8Black),
            new Chunk("     ", Fonts.Helvetica8Black).SetBackground(Colors.AutoSetNc),
            new Chunk(" ", Fonts.Helvetica8Black),
            new Chunk("Point de contrôle non ciblé", Fonts.Helvetica8BlackBoldItalic)
        };

        #region Members

        private readonly InspectionPdfModel model_;

        #endregion

        #region Initialization

        public InspectionPdf(InspectionPdfModel model, string username, bool showWatermark = false) : base(username)
        {
            model_ = model;
            Size = PageSize.A4;
            Landscape = false;
            TopMargin = 50;
            BottomMargin = 50;
            LeftMargin = RightMargin = 30;
            Watermark = showWatermark ? WatermarkText : "";
            LightColor = Colors.LightGray;
            DarkColor = Colors.Gray;
            PageNumberPosition = 5;
        }

        #endregion

        #region Services

        protected override void AddBody(PdfWriter writer, Document document)
        {
            document.Add(Title());
            document.Add(FarmAndOrganization());
            document.Add(RubricsSummary());
            document.Add(Signatures());
            document.Add(Objection());
            document.NewPage();
            document.Add(CheckList());
        }

        private AcPdfPTable Title()
        {
            var table = CustomTable(new[] {55f, 45f});
            table.AddCustomCell($"Protocole de constat du contrôle {model_.CampaignYear}", Fonts.Helvetica14BlackBold,
                borderWidth: 0);
            AddImageCellFromBase64(table, FocaaBase64, 65, 2, 0, Element.ALIGN_RIGHT);
            table.AddCustomCell(model_.DomainName, Fonts.Helvetica18BlackBold, borderWidth: 0);
            return table;
        }

        private AcPdfPTable FarmAndOrganization()
        {
            var table = CustomTable(new[] {45f, 10f, 45f});
            table.AddCustomCell("Exploitation", Fonts.Helvetica12BlackBold, borderWidth: 0, colspan: 2);
            table.AddCustomCell("Organisation ayant effectué le contrôle", Fonts.Helvetica10BlackBold, borderWidth: 0);
            AddFarmCell(table, model_.Farm);
            table.AddCustomCell(" ", borderWidth: 0);
            AddOrganizationCell(table);
            return table;
        }

        private AcPdfPTable RubricsSummary()
        {
            var table = CustomTable(new[] {15f, 35f, 5f, 45f});
            var data = model_.InspectionResults.Where(x => x.ResultType == ResultModel.ResultTypes.Rubric);
            table.AddCustomCell("Résumé du contrôle des exigences", Fonts.Helvetica12BlackBold, borderWidth: 0,
                colspan: 4);
            table.AddTitleCell("Règle N°");
            table.AddTitleCell("Exigence");
            table.AddTitleCell("Validation");
            table.AddTitleCell("Commentaire");

            Phrase Comment(InspectionOutcome outcome, string inspectorComment)
            {
                var result = new Phrase {new Chunk(inspectorComment, Fonts.Helvetica8BlackBoldItalic)};
                if (outcome == InspectionOutcome.NotOk || outcome == InspectionOutcome.PartiallyOk)
                {
                    if (!string.IsNullOrWhiteSpace(inspectorComment))
                        result.Add(new Chunk("\n"));

                    result.Add(new Chunk("Voir détails ci-dessous", Fonts.Helvetica8Black));
                }

                return result;
            }
            foreach (var resultItem in data)
            {
                table.AddCustomCell(resultItem.ConjunctElementCode, Fonts.Helvetica8Black, BackgroundColor(resultItem));
                table.AddCustomCell(resultItem.ShortName, Fonts.Helvetica8Black, BackgroundColor(resultItem));
                table.AddCustomCell(OutcomeString(resultItem.ResultOutcome), Fonts.Helvetica8Black,
                    OutcomeBackgroundColor(resultItem));
                table.AddCustomCell(Comment(resultItem.ResultOutcome, resultItem.ResultInspectorComment),
                    OutcomeDetailsBackgroundColor(resultItem, Colors.White));
            }

            table.AddTitleCell("Remarque générale", colspan: 4);
            table.AddCustomCell(string.IsNullOrWhiteSpace(model_.CommentForFarmer) ? " " : model_.CommentForFarmer,
                colspan: 4, font: Fonts.Helvetica8BlackItalic);
            table.AddTitleCell("Documents à livrer", colspan: 4);
            table.AddCustomCell(
                string.IsNullOrWhiteSpace(model_.ActionsOrDocuments) ? "Aucun" : model_.ActionsOrDocuments, colspan: 4,
                font: Fonts.Helvetica8BlackItalic);
            if (model_.DueDate.HasValue)
                table.AddCustomCell(new Phrase
                    {
                        new Chunk("Délai: ", Fonts.Helvetica8Black),
                        new Chunk(model_.DueDate.Value.ToShortDateString(), Fonts.Helvetica8BlackBoldItalic)
                    },
                    colspan: 4);

            table.AddCustomCell(OutcomeKey, colspan: 4, borderWidth: 0);
            table.AddCustomCell(ColorsKey, colspan: 4, borderWidth: 0);
            return table;
        }

        public AcPdfPTable Signatures()
        {
            var table = CustomTable(new[] {30f, 70f});

            void DisplaySignatureOrEmpty(string signature)
            {
                if (!string.IsNullOrWhiteSpace(signature) && signature.StartsWith(PngImgMarker))
                {
                    AddImageCellFromBase64(table, signature.Split(',')[1], 33);
                }
                else
                {
                    table.AddCustomCell("\n\n\n\n\n");
                }
            }

            table.AddCustomCell(
                "L'exploitant ou son représentant atteste avoir pris connaissance du présent rapport de contrôle.",
                Fonts.Helvetica12BlackBold, colspan: 2, borderWidth: 0);
            table.AddTitleCell(model_.HasProxy
                ? "[  ] L'exploitant ou [x] son représentant"
                : "[x] L'exploitant ou [  ] son représentant");
            table.AddTitleCell("Signature");
            table.AddCustomCell(!string.IsNullOrWhiteSpace(model_.ProxyName)
                ? model_.ProxyName
                : model_.Farm.CompleteName);
            DisplaySignatureOrEmpty(model_.FarmerSignatureImage);
            table.AddTitleCell("Contrôleur (ou gérant(e))");
            table.AddTitleCell("Signature");
            table.AddCustomCell(model_.DoneByInspector);
            DisplaySignatureOrEmpty(model_.InspectorSignatureImage);
            if (!string.IsNullOrWhiteSpace(model_.Inspector2) &&
                !string.IsNullOrWhiteSpace(model_.Inspector2SignatureImage))
            {
                table.AddTitleCell("Contrôleur ou préposé additionnel");
                table.AddTitleCell("Signature");
                table.AddCustomCell(model_.Inspector2);
                DisplaySignatureOrEmpty(model_.Inspector2SignatureImage);
            }

            if (model_.DoneOn.HasValue)
                //table.AddCustomCell($"Fait à {model_.DoneInTownDisplay} le {model_.DoneOn.Value.ToShortDateString()}", colspan: 2);
                table.AddCustomCell($"Fait le {model_.DoneOn.Value.ToShortDateString()}", colspan: 2);

            return table;
        }

        private AcPdfPTable Objection()
        {
            Phrase OrganizationPhrase(Organization org)
            {
                return new Phrase
                {
                    new Chunk($"{org.CantonCode}: ", Fonts.Helvetica8BlackBold),
                    new Chunk($"{org.Name}: {org.Address}", Fonts.Helvetica8Black)
                };
            }

            var table = CustomTable(new[] {50f, 50f});
            table.AddCustomCell(
                "En cas de contestation, une réclamation écrite avec les points contestés peut être adressée dans les 3 jours ouvrables suivant le contrôle à l’organisme d’inspection ayant effectué le contrôle:",
                Fonts.Helvetica8BlackBold, colspan: 2, borderWidth: 0);
            table.AddCustomCell(OrganizationPhrase(Organization.Agripige), borderWidth: 0);
            table.AddCustomCell(OrganizationPhrase(Organization.Cobra), borderWidth: 0);
            table.AddCustomCell(OrganizationPhrase(Organization.Anapi), borderWidth: 0);
            table.AddCustomCell(OrganizationPhrase(Organization.Ajapi), borderWidth: 0);
            return table;
        }

        private AcPdfPTable CheckList()
        {
            var data = model_.InspectionResults.OrderBy(x => x.ConjunctElementCode).ThenBy(x=> x.TreeLevel);
            var table = CustomTable(new[] {4f, 4f, 4f, 4f, 40f, 5f, 40f});
            table.AddCustomCell("Résultat détaillé du contrôle des exigences", Fonts.Helvetica12BlackBold,
                borderWidth: 0, colspan: 7);
            table.AddTitleCell("Règle N°", colspan: 4);
            table.AddTitleCell("Exigences");
            table.AddTitleCell("Validation");
            table.AddTitleCell("Commentaire");

            bool isFirstRubric = true;
            foreach (var item in data)
            {
                var font = item.TreeLevel == 0 ? Fonts.Helvetica11BlackBold :
                    item.TreeLevel == 1 ? Fonts.Helvetica10BlackBold :
                    item.TreeLevel == 2 ? Fonts.Helvetica9BlackBold :
                    item.TreeLevel == 3 ? Fonts.Helvetica8BlackBold :
                    Fonts.Helvetica8Black;
                var colspan = 4 - item.TreeLevel;
                float borderWidth = item.ResultType == ResultModel.ResultTypes.Point ? 0f : 0.5f;

                switch (item.TreeLevel)
                {
                    case 0 when !isFirstRubric:
                        table.AddCustomCell(" ", font, borderWidth: 0f, colspan: 7);
                        break;
                    case 1:
                    case 2:
                    case 3:
                        table.AddCustomCell("", colspan: item.TreeLevel, borderWidth: 0f);
                        break;
                }

                table.AddCustomCell(item.ElementCode, font, borderWidth: borderWidth, colspan: colspan,
                    backgroundColor: BackgroundColor(item));
                table.AddCustomCell(item.ShortName, font, borderWidth: borderWidth,
                    backgroundColor: BackgroundColor(item));
                table.AddCustomCell(OutcomeString(item.ResultOutcome), borderWidth: borderWidth,
                    backgroundColor: OutcomeBackgroundColor(item));
                AddResultCell(table, item, borderWidth, OutcomeDetailsBackgroundColor(item, Colors.White));
                isFirstRubric = false;
            }

            table.AddCustomCell(" ", colspan: 7, borderWidth: 0); // empty line
            table.AddCustomCell(OutcomeKey, colspan: 7, borderWidth: 0);
            table.AddCustomCell(ColorsKey, colspan: 7, borderWidth: 0);

            return table;
        }

        private void AddOrganizationCell(AcPdfPTable table)
        {
            var cell = DefaultCell(table.ColorDark, 0);

            Phrase OrganizationPhrase(Organization organization)
            {
                bool check = model_.OrganizationName == organization.Name;
                return new Phrase
                {
                    new Chunk(check ? "[x] " : "[  ] ", Fonts.Helvetica10BlackBold),
                    new Chunk(organization.Name + ": ", Fonts.Helvetica10BlackBold),
                    new Chunk(organization.Address, Fonts.Helvetica10Black)
                };
            }

            cell.AddElement(OrganizationPhrase(Organization.Agripige));
            cell.AddElement(OrganizationPhrase(Organization.Ajapi));
            cell.AddElement(OrganizationPhrase(Organization.Anapi));
            cell.AddElement(OrganizationPhrase(Organization.Cobra));

            table.AddCell(cell);
        }

        private void AddFarmCell(AcPdfPTable table, FarmModel farm)
        {
            var cell = DefaultCell(table.ColorDark);
            cell.AddElement(new Phrase(farm.Ktidb, Fonts.Helvetica12BlackBold));
            cell.AddElement(new Phrase(farm.CompleteName, Fonts.Helvetica12BlackBold));
            cell.AddElement(new Phrase(farm.Address, Fonts.Helvetica10Black));
            cell.AddElement(new Phrase("Email: " + farm.Email, Fonts.Helvetica10Black));
            table.AddCell(cell);
        }

        private void AddResultCell(AcPdfPTable table, ResultModel resultModel, float borderWidth,
            BaseColor backgroundColor = null)
        {
            backgroundColor ??= Colors.White;
            var cell = DefaultCell(table.ColorDark, borderWidth, backgroundColor);
            if (!string.IsNullOrWhiteSpace(resultModel.ResultInspectorComment))
                cell.AddElement(new Phrase
                {
                    new Chunk("Remarque contrôleur: ", Fonts.Helvetica8Black),
                    new Chunk(resultModel.ResultInspectorComment, Fonts.Helvetica8BlackBoldItalic),
                });

            if (!string.IsNullOrWhiteSpace(resultModel.ResultFarmerComment))
                cell.AddElement(new Phrase
                {
                    new Chunk("Remarque exploitant: ", Fonts.Helvetica8Black),
                    new Chunk(resultModel.ResultFarmerComment, Fonts.Helvetica8BlackBoldItalic),
                });

            if (resultModel.HasDefect)
                cell.AddElement(new Phrase
                {
                    new Chunk("Manquement dans la liste: ", Fonts.Helvetica8Black),
                    new Chunk(resultModel.DefectName, Fonts.Helvetica8BlackBoldItalic),
                });

            if (!string.IsNullOrWhiteSpace(resultModel.ResultDefectDescription))
                cell.AddElement(new Phrase
                {
                    new Chunk("Manquement constaté: ", Fonts.Helvetica8Black),
                    new Chunk(resultModel.ResultDefectDescription, Fonts.Helvetica8BlackBoldItalic),
                });

            if (resultModel.ResultSize.HasValue)
                cell.AddElement(new Phrase
                {
                    new Chunk("Ampleur du manquement: ", Fonts.Helvetica8Black),
                    new Chunk(resultModel.ResultSize.ToString() + " " + resultModel.ResultUnit, Fonts.Helvetica8BlackBoldItalic),
                });

            if (resultModel.Seriousness != DefectSeriousness.Empty) //TODO a verifier
                cell.AddElement(new Phrase
                {
                    new Chunk("Gravité: ", Fonts.Helvetica8Black),
                    new Chunk(resultModel.Seriousness?.Name, Fonts.Helvetica8BlackBoldItalic),
                });

            //if (resultModel.Repetition != DefectRepetitions.NoDefect)
            //    cell.AddElement(new Phrase($"Récidive: {resultModel.Repetition.ToDisplayName()}", Fonts.Helvetica8Black));
            
            table.AddCell(cell);
        }

        private static PdfPCell DefaultCell(BaseColor borderColor, float borderWidth = 0.5f,
            BaseColor backgroundColor = null)
        {
            backgroundColor ??= Colors.White;
            return new PdfPCell
            {
                MinimumHeight = 0.0f,
                BackgroundColor = backgroundColor,
                Colspan = 1,
                Rowspan = 1,
                BorderWidth = borderWidth,
                BorderColor = borderColor,
                VerticalAlignment = 5,
                HorizontalAlignment = 0
            };
        }

        private string OutcomeString(InspectionOutcome outcome)
        {
            if (outcome == InspectionOutcome.Unset)
                return "";

            return outcome == InspectionOutcome.Ok ? "Oui" :
                outcome == InspectionOutcome.PartiallyOk ? "P" :
                outcome == InspectionOutcome.NotOk ? "Non" :
                outcome == InspectionOutcome.NotApplicable ? "NA" :
                outcome == InspectionOutcome.NotInspected ? "NC" : "";
        }

        private BaseColor OutcomeBackgroundColor(ResultModel item, BaseColor defaultColor = null)
        {
            defaultColor ??= BackgroundColor(item, Colors.LightGray);
            var okColor = Colors.Ok;
            var koColor = Colors.Ko;
            var pokColor = Colors.Pok;

            if (item.ResultOutcome == InspectionOutcome.Unset) return defaultColor;
            if (item.ResultOutcome == InspectionOutcome.NotOk) return koColor;
            if (item.ResultOutcome == InspectionOutcome.PartiallyOk) return pokColor;
            if (item.ResultOutcome == InspectionOutcome.Ok) return okColor;
            if (item.ResultOutcome == InspectionOutcome.NotApplicable) return defaultColor;
            if (item.ResultOutcome == InspectionOutcome.NotInspected) return defaultColor;

            return defaultColor;
        }

        private BaseColor OutcomeDetailsBackgroundColor(ResultModel item, BaseColor defaultColor = null)
        {
            defaultColor ??= BackgroundColor(item, Colors.LightGray);
            var koColor = Colors.Ko;
            var pokColor = Colors.Pok;

            if (item.ResultOutcome == InspectionOutcome.Unset) return defaultColor;
            if (item.ResultOutcome == InspectionOutcome.NotOk) return koColor;
            if (item.ResultOutcome == InspectionOutcome.PartiallyOk) return pokColor;
            if (item.ResultOutcome == InspectionOutcome.Ok) return defaultColor;
            if (item.ResultOutcome == InspectionOutcome.NotApplicable) return defaultColor;
            if (item.ResultOutcome == InspectionOutcome.NotInspected) return defaultColor;

            return defaultColor;
        }

        private BaseColor BackgroundColor(ResultModel item, BaseColor defaultColor = null)
        {
            defaultColor ??= Colors.White;

            if (item.HasAutoSetAncestor && item.ResultOutcome == InspectionOutcome.NotApplicable)
                return Colors.AutoSetNa;

            if (item.HasAutoSetAncestor && item.ResultOutcome == InspectionOutcome.NotInspected)
                return Colors.AutoSetNc;

            return defaultColor;
        }

        private void AddImageCellFromFile(AcPdfPTable table,
            string imagePath,
            int scalePercent = 100,
            int rowspan = 1,
            float borderWidth = 0.5f,
            int horizontalAlignment = Element.ALIGN_LEFT)
        {
            if (string.IsNullOrEmpty(imagePath))
                return;

            var image = Image.GetInstance(imagePath); // PlatformNotSupportedException while calling Image.GetInstance(url) which calls System.Net.WebRequest.Create.
                                                      // Must directly pass byte array of logo image instead.

            image.ScalePercent(scalePercent);
            AddImageCell(table, image, rowspan, borderWidth, horizontalAlignment);

        }

        private void AddImageCellFromBase64(AcPdfPTable table,
            string base64,
            int scalePercent = 100,
            int rowspan = 1,
            float borderWidth = 0.5f,
            int horizontalAlignment = Element.ALIGN_LEFT)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            var image = Image.GetInstance(bytes);
            image.ScalePercent(scalePercent);
            AddImageCell(table, image, rowspan, borderWidth, horizontalAlignment);
        }

        private void AddImageCell(AcPdfPTable table,
            Image image,
            int rowspan = 1,
            float borderWidth = 0.5f,
            int horizontalAlignment = Element.ALIGN_LEFT)
        {
            var cell = new PdfPCell(image, false)
            {
                BorderWidth = borderWidth,
                BorderColor = table.ColorDark,
                Rowspan = rowspan,
                HorizontalAlignment = horizontalAlignment
            };
            table.AddCell(cell);
        }

        #endregion

        #region Helper

        public static string Filename(int year, string ktidb, string farmName, string domainShortName)
        {
            return $"Rapport de contrôle {year} {ktidb} {farmName} {domainShortName}.pdf";
        }

        #endregion
    }
}
