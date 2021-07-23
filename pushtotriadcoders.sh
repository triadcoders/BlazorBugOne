#bash -c "rsync -avH --delete --progress /mnt/d/PROJECTS/BlazorBugOne -e ssh mrdefault@triadcoders.com:/home/mrdefault/BBO/PUSH/"
#bash -c "rsync -avH -v --progress /mnt/d/PROJECTS/BlazorBugOne -e ssh mrdefault@triadcoders.com:/home/mrdefault/BBO/PUSH/"
bash -c "rsync -avH --progress /mnt/d/PROJECTS/BlazorBugOne -e ssh mrdefault@triadcoders.com:/home/mrdefault/BBO/PUSH/"


