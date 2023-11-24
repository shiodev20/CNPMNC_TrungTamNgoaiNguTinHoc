<template>
  <div class="add_exam">
    <h1>
      {{ this.$route.query.action == "edit" ? "Cập nhật" : "Thêm" }} đợt thi
    </h1>
    <form @submit.prevent="onsubmit" class="from_add_exambatch">
      <div class="group_add_exam">
        <div class="name_add_exam">
          <label>Tên đợt thi</label>
          <input v-model="data.name" type="text" />
          <span class="err" v-if="err.name">{{ err.name }}</span>
        </div>
        <!-- dropdown lanquage  -->
        <div class="language_add_exam">
          <label>Tên kỳ Thi </label>
          <div class="dropdowns">
            <div
              @click="status_dropdown_exambatch = !status_dropdown_exambatch"
              class="dropdown_input"
            >
              <span class="text">
                {{
                  data.exam_id && exam
                    ? $store.getters["exam/getbyid"](data.exam_id).name
                    : "Chọn kỳ thi"
                }}</span
              >
              <font-awesome-icon class="icon" :icon="['fas', 'caret-down']" />
            </div>
            <div
              :class="
                status_dropdown_exambatch == true ? 'dropdown_menu_show' : null
              "
              class="dropdown_menu"
            >
              <div
                @click="
                  (data.exam_id = item.id), (status_dropdown_exambatch = false)
                "
                class="dropdown_item"
                v-for="item in exam"
                :key="item.id"
              >
                {{ item.name }}
              </div>
            </div>
          </div>
          <span class="err" v-if="err.exam_id">{{ err.exam_id }}</span>
        </div>
        <!-- dropdown lanquage -->
      </div>
      <div v-if="data.exam_id">
        <div
          v-for="(item, index) in check_type_exam.exam_type_id"
          :key="item.id"
          class="group_time_1"
        >
          <label class="title_1">Phần :{{ content_exam_id(item).name }}</label>
          <div class="group_time_1_child">
            <div class="group_time_1_child_item">
              <label>Giờ bắt đầu</label>
              <input v-model="data.exam_data[index].start_hour" type="time" />
              <span class="err" v-if="err.exam_data[index].start_hour">{{
                err.exam_data[index].start_hour
              }}</span>
            </div>
            <div class="group_time_1_child_item">
              <label>Giờ kết thúc</label>
              <input v-model="data.exam_data[index].end_hour" type="time" />
              <span class="err" v-if="err.exam_data[index].end_hour">{{
                err.exam_data[index].end_hour
              }}</span>
            </div>
            <div class="group_time_1_child_item">
              <label>Ngày Thi</label>
              <input v-model="data.exam_data[index].start_date" type="date" />
              <span class="err" v-if="err.exam_data[index].start_date">{{
                err.exam_data[index].start_date
              }}</span>
            </div>
            <div class="dropdown group_time_1_child_item">
              <label> Địa điểm </label>
              <div class="dropdown_from">
                <div
                  @click="
                    status_dropdown_branch[index].drop =
                      !status_dropdown_branch[index].drop
                  "
                  class="dropdown_input_from"
                >
                  <span
                    v-if="data.exam_data[index].branch_id != null"
                    class="text"
                  >
                    {{
                      $store.getters["branch/getbyid"](
                        data.exam_data[index].branch_id
                      )[0].name
                    }}</span
                  >
                  <font-awesome-icon
                    class="icon"
                    :icon="['fas', 'caret-down']"
                  />
                </div>
                <div
                  :class="
                    status_dropdown_branch[index].drop == true
                      ? 'dropdown_menu_show'
                      : null
                  "
                  class="dropdown_menu"
                >
                  <div
                    @click="
                      (data.exam_data[index].branch_id = item.id),
                        (status_dropdown_branch[index].drop = false)
                    "
                    class="dropdown_item"
                    v-for="item in brach"
                    :key="item.id"
                  >
                    {{ item.name }}
                  </div>
                </div>
              </div>
              <span class="err" v-if="err.exam_data[index].branch_id">{{
                err.exam_data[index].branch_id
              }}</span>
            </div>
          </div>
        </div>
      </div>
      <!-- button fished -->
      <div class="btn_fished">
        <button>
          {{ this.$route.query.action == "edit" ? "Cập nhật" : "Hoàn tất" }}
        </button>
      </div>
      <!-- end button fished -->
    </form>
  </div>
</template>
<script>
import { computed, reactive, ref } from "vue";
import { useToast } from "vue-toastification";
import store from "@/store";
export default {
  name: "HomeView",
  created() {
    const check = setInterval(() => {
      if (store.state.exambatch.exambatch != null) {
        clearInterval(check);
        if (this.$route.query.action) {
          if (this.$route.query.action == "edit") {
            store.state.exambatch.data_post.id = Number(this.$route.query.id);
            store.state.exambatch.data_post.name = store.getters[
              "exambatch/getbyid"
            ](Number(this.$route.query.id))[0].name;
            store.state.exambatch.data_post.exam_id = store.getters[
              "exambatch/getbyid"
            ](Number(this.$route.query.id))[0].exam_id;
          }
        }
      }
    }, 200);
  },
  setup() {
    const toast = useToast();
    store.dispatch("exambatch/get_all");
    store.dispatch("exam/get_all");
    store.dispatch("branch/get_all");
    store.dispatch("content_exame/get_all");
    const content_exame = computed(() => {
      return store.state.content_exame.content_exame;
    });
    const content_exam_id = (value) => {
      return store.getters["content_exame/getbyid"](value)[0];
    };
    const data_fetch = reactive({
      name: null,
      id: null,
    });
    const status_dropdown_exambatch = ref(false);
    var status_dropdown_branch = ref([]);
    const err = computed(() => {
      return store.state.exambatch.err;
    });
    const convert_time_check = (time) => {
      const datasx = new Date();
      datasx.setHours(Number(time.split(":")[0]));
      datasx.setMinutes(Number(time.split(":")[1]));
      return datasx.getTime();
    };
    const reset_data = () => {
      data.value.name = null;
      data.value.exam_id = null;
    };
    const onsubmit = () => {
      data.value.name == null || data.value.name == ""
        ? (err.value.name = "Không bỏ trống")
        : (err.value.name = null);
      data.value.exam_id == null
        ? (err.value.exam_id = "Không bỏ trống")
        : (err.value.exam_id = null);
      if (data.value.exam_id != null) {
        if (check_type_exam.value.exam_type_id.length > 0) {
          // check
          for (var item in check_type_exam.value.exam_type_id) {
            run(item);
          }
          var chexck = true;
          for (var items in check_type_exam.value.exam_type_id) {
            if (run_validate(items) == false) {
              chexck = false;
            }
          }
          console.log(chexck);
          if (chexck == true) {
            const datas = {
              id: data.value.id,
              name: data.value.name,
              exam_id: data.value.exam_id,
              status: true,
              exam_data: data.value.exam_data,
            };
            if (data.value.id == null) {
              store.dispatch("exambatch/create", datas);
              toast.success("Tạo Thành công");
              reset_data();
            } else {
              if (data.value.name == "") {
                toast.error("Cập nhật thất bại");
              } else {
                store.dispatch("exambatch/update", datas);
                toast.success("Cập nhật thành công");
              }
            }
          } else {
            data.value.id == null
              ? toast.error("Tạo thất bại")
              : toast.error("Cập nhật thất bại");
          }
        }
      } else {
        data.value.id == null
          ? toast.error("Tạo thất bại")
          : toast.error("Cập nhật thất bại");
      }
    };
    const run_validate = (index) => {
      if (
        err.value.exam_data[index].start_hour != null ||
        err.value.exam_data[index].end_hour != null ||
        err.value.exam_data[index].start_date != null ||
        err.value.exam_data[index].branch_id != null ||
        (err.value.exam_id != null && err.value.name != null) ||
        (err.value.exam_id != null && err.value.name != "")
      ) {
        return false;
      }
      return true;
    };
    const run = (index) => {
      data.value.exam_data[index].start_hour == null
        ? (err.value.exam_data[index].start_hour = "Không bỏ trống")
        : (err.value.exam_data[index].start_hour = null);

      data.value.exam_data[index].end_hour == null
        ? (err.value.exam_data[index].end_hour = "Không bỏ trống")
        : convert_time_check(data.value.exam_data[index].end_hour) <=
          convert_time_check(data.value.exam_data[index].start_hour)
        ? (err.value.exam_data[index].end_hour = "Không hợp lệ")
        : (err.value.exam_data[index].end_hour = null);

      data.value.exam_data[index].start_date == null
        ? (err.value.exam_data[index].start_date = "Không bỏ trống")
        : new Date(data.value.exam_data[index].start_date).getTime() <=
          new Date().getTime()
        ? (err.value.exam_data[index].start_date = "Ngày không hợp lệ")
        : (err.value.exam_data[index].start_date = null);

      data.value.exam_data[index].branch_id == null
        ? (err.value.exam_data[index].branch_id = "Không bỏ trống")
        : (err.value.exam_data[index].branch_id = null);
    };
    const exam = computed(() => {
      return store.state.exam.exam;
    });
    const data = computed(() => {
      return store.state.exambatch.data_post;
    });
    const brach = computed(() => {
      return store.state.branch.branch;
    });
    const check_type_exam = computed(() => {
      const x = exam.value.filter((e) => e.id == data.value.exam_id)[0]
        .exam_type_id;
      store.state.exambatch.data_post.exam_data = [];
      store.state.exambatch.err.exam_data = [];
      // eslint-disable-next-line vue/no-side-effects-in-computed-properties
      status_dropdown_branch.value = [];
      for (var main in x) {
        var datasx = reactive({
          start_hour: null,
          end_hour: null,
          exam_type: x[main],
          start_date: null,
          branch_id: null,
        });
        var errs = reactive({
          start_hour: null,
          end_hour: null,
          start_date: null,
          branch_id: null,
        });
        var datasx01 = reactive({
          drop: null,
        });
        // eslint-disable-next-line vue/no-side-effects-in-computed-properties
        status_dropdown_branch.value.push(datasx01);
        store.state.exambatch.data_post.exam_data.push(datasx);
        store.state.exambatch.err.exam_data.push(errs);
        datasx = null;
        errs = null;
      }
      if (store.state.exambatch.data_post.id) {
        store.state.exambatch.data_post = store.getters["exambatch/getbyid"](
          store.state.exambatch.data_post.id
        )[0];
      }
      return exam.value.filter((e) => e.id == data.value.exam_id)[0];
    });
    return {
      check_type_exam,
      data,
      brach,
      onsubmit,
      err,
      data_fetch,
      exam,
      status_dropdown_exambatch,
      status_dropdown_branch,
      content_exame,
      content_exam_id,
    };
  },
};
</script>
<style scoped>
.group_add_exam > div {
  position: relative;
}
.err {
  position: absolute;
  top: calc(100% + 10px);
  font-size: 14px;
  color: red;
}
.dropdowns {
  position: relative;
  z-index: 100;
  background: white;
}
.group_add_exam > div {
  display: flex !important;
  align-items: start;
  gap: 5px;
  flex-direction: column;
}
.group_add_exam > div > input,
.group_add_exam > div > div {
  width: 100% !important;
}
/* end group name and language */
h1 {
  color: var(--color_main);
  text-align: center;
  font-family: Roboto;
  font-size: 24px;
  font-style: normal;
  font-weight: 600;
  line-height: normal;
}

.group_add_exam {
  display: grid;
  grid-template-columns: 330px 330px;
  gap: 20px;
  margin-bottom: 20px;
}
h1 {
  color: #071952;
  text-align: center;
  font-family: Roboto;
  font-size: 24px;
  font-style: normal;
  font-weight: 600;
  line-height: normal;
}
.from_add_exambatch {
  background: white;
  padding: 30px 50px;
  border-radius: 10px;
}
/* button fished */
.btn_fished > button {
  border-radius: 10px;
  background: rgb(187, 4, 4);
  display: flex;
  width: 172px;
  height: 44px;
  padding: 5px 11px;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  gap: 10px;
  color: white;
  border: none;
}
.btn_fished {
  padding: 60px;
  display: flex;
  justify-content: center;
  align-items: center;
}
/* and button fished */
/* end drop down  */

/* name exambatch */
.name_exambatch {
  display: flex;
  flex-direction: column;
  align-items: flex-start;
}
.name_exambatch > label {
  margin-bottom: 5px;
  color: var(--color_main);
  text-align: center;
  font-family: Roboto;
  font-size: 14px;
  font-style: normal;
  font-weight: 600;
  line-height: normal;
}
.name_exambatch > input {
  width: 294px;
  height: var(--size-field_height);
  flex-shrink: 0;
  border-radius: 10px;
  border: 1px solid var(--color_main);
  text-align: center;
  font-family: Roboto;
  font-size: 16px;
  font-style: normal;
  font-weight: 600;
  line-height: normal;
}
/* end name exambatch */

/* group list */
.title_1 {
  display: block;
  padding: 20px 0;
  color: #071952;
  font-family: Roboto;
  font-size: 14px;
  font-style: normal;
  font-weight: 600;
  line-height: normal;
}
.group_time_1 {
  text-align: start;
  padding-top: 20px;
}
.group_time_1_child {
  display: flex;
  gap: 20px;
  justify-content: space-around;
}
.group_time_1_child > div {
  display: flex;
  flex-direction: column;
  position: relative;
}
.group_time_1_child_item > label {
  margin-bottom: 5px;
  color: var(--color_main);
  text-align: center;
  font-family: Roboto;
  font-size: 14px;
  font-style: normal;
  font-weight: 600;
  line-height: normal;
}
.group_time_1_child_item > input {
  width: 123px;
  height: 32px;
  flex-shrink: 0;
  border-radius: 5px;
  border: 1px solid var(--color_main);
  padding: 0 10px;
}
/* end group list  */

/* dropdown  */
/* drop down  */
.dropdown_input > .icon {
  position: absolute;
  right: 10px;
  top: 50%;
  transform: translateY(-50%);
}
.dropdown_input_from {
  position: relative;
}
.dropdown_input_from > .text {
  position: absolute;
  top: 50%;
  left: 50%;
  color: var(--color_main);
  text-align: center;
  font-family: Roboto;
  font-size: 15px !important;
  font-style: normal;
  font-weight: 500;
  line-height: normal;
  transform: translate(-50%, -50%);
  white-space: nowrap;
}
.text {
  position: absolute;
  top: 50%;
  left: 50%;
  color: var(--color_main);
  text-align: center;
  font-family: Roboto;
  font-size: 15px !important;
  font-style: normal;
  font-weight: 500;
  line-height: normal;
  transform: translate(-50%, -50%);
  white-space: nowrap;
}
.dropdown_input_from > .icon {
  position: absolute;
  right: 10px;
  top: 50%;
  transform: translateY(-50%);
}
.dropdown_menu_show {
  opacity: 1 !important;
  pointer-events: visible !important;
}
.dropdown_input {
  position: relative;
  height: var(--size-field_height);
  background: white;
  border-radius: 10px;
  border: 1px solid var(--color_main);
  color: var(--color_main);
  font-weight: 600;
}
.dropdown_menu {
  transition: 0.1s;
  z-index: 10;
  margin-top: 3px;
  border: 1px solid var(--color_main);
  position: absolute;
  left: 0;
  right: 0;
  opacity: 0;
  pointer-events: none;
}
.dropdown_item {
  padding: 10px;
  background: white;
}
.dropdown_item:hover {
  background: var(--color_main);
  color: white;
}
.dropdown_input_from {
  width: 123px;
  height: 32px;
  border-radius: 5px;
  border: 1px solid var(--color_main);
}
input {
  color: var(--color_main) !important;
  font-family: Roboto;
  font-size: 16px;
  font-style: normal;
  font-weight: 400;
  line-height: normal;
  height: var(--size-field_height);
  border-radius: 10px;
  border: 1px solid var(--color_main);
}
/* end dropdown */
</style>
