using EnvDTE;
using Microsoft.Internal.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TemplateWizard;
using Microsoft.VisualStudio.Threading;
using SampleUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleInstaller
{
    public class SolutionWizard : IWizard
    {

        private readonly AsyncLazy<IVsUIShell> _uiShell = new AsyncLazy<IVsUIShell>(
            async () =>
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                return ServiceProvider.GlobalProvider.GetService(typeof(SVsUIShell)) as IVsUIShell;
            },
            ThreadHelper.JoinableTaskFactory);

        protected void Initialize()
        {

                
        }

        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
        }

        public void RunFinished()
        {

        }



          
        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {

            if (runKind == WizardRunKind.AsNewProject || runKind == WizardRunKind.AsMultiProject)
            {
                ShowModal();
            }

        }

            

        public SolutionWizard()
        {
        }

        public void ShowModal()
        {
            ThreadHelper.JoinableTaskFactory.Run(async () =>
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

                // get the owner of this dialog
                var uiShell = await _uiShell.GetValueAsync();
                uiShell.GetDialogOwnerHwnd(out IntPtr hwnd);

                var dialog = new MainWindow();
                dialog.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;

                uiShell.EnableModeless(0);

                try
                {
                    WindowHelper.ShowModal(dialog, hwnd);
                }
                finally
                {
                    // This will take place after the window is closed.
                    uiShell.EnableModeless(1);
                }
            });

    }

        public bool ShouldAddProjectItem(string filePath)
        {
            throw new NotImplementedException();
        }
    }
 }
