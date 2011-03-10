# vim: tabstop=4 shiftwidth=4 softtabstop=4
# 
#  Copyright (c) 2011 Openstack, LLC.
#  All Rights Reserved.
# 
#     Licensed under the Apache License, Version 2.0 (the "License"); you may
#     not use this file except in compliance with the License. You may obtain
#     a copy of the License at
# 
#          http://www.apache.org/licenses/LICENSE-2.0
# 
#     Unless required by applicable law or agreed to in writing, software
#     distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
#     WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
#     License for the specific language governing permissions and limitations
#     under the License.
#

"""
JSON misc commands plugin
"""

from plugins.jsonparser import jsonparser
import debian

class network_commands(jsonparser.command):

    def __init__(self, *args, **kwargs):
        super(jsonparser.command, self).__init__(*args, **kwargs)

    @jsonparser.command_add('resetnetwork')
    def resetnetwork_cmd(self, data):
        # XXX -- Need to figure out our OS.. 
        debian.network.write_interfaces(data)
        return (0, "")
